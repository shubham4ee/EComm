using UserServer.DAL.DataContext;
using UserServer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.DAL.Repositories
{
    public class CADFileRepository : GenericRepository<CADFile> , ICADFileRepository
    {
        private readonly ApplicationDbContext _context;
        public readonly DbSet<CADFile> _CADFilesDbSet;

        public CADFileRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _CADFilesDbSet = _context.CADFiles;
        }
        public async Task AddRangeAsync(List<CADFile> cadfiles)
        {
            try
            {
                // use this when duplicate records are allowed 
                    //await _CADFilesDbSet.AddRangeAsync(cadfiles);
                    //await _context.SaveChangesAsync();
                // use this when update existing records and insert new one
                // Upsert operation
                var insertQuery = @"
                                INSERT INTO ""CADFiles"" (""ProjectId"", ""FileName"", ""FilePath"", ""UploadedBy"", ""CreatedAt"", ""ModifiedAt"", ""ConversionStatus"")
                                VALUES (@ProjectId, @FileName, @FilePath, @UploadedBy, @CreatedAt, @ModifiedAt, COALESCE(@ConversionStatus, 'Not Initiated'))
                                ON CONFLICT (""ProjectId"", ""FileName"") 
                                DO UPDATE SET 
                                ""FilePath"" = EXCLUDED.""FilePath"", 
                                ""UploadedBy"" = EXCLUDED.""UploadedBy"",
                                ""ModifiedAt"" = EXCLUDED.""ModifiedAt"",
                                ""ConversionStatus"" = EXCLUDED.""ConversionStatus"";";

                // Iterate over the list of CAD files and execute the upsert for each
                foreach (var cadFile in cadfiles)
                {
                    // Execute raw SQL for each CAD file 
                    await _context.Database.ExecuteSqlRawAsync(insertQuery,
                        new NpgsqlParameter("@ProjectId", cadFile.ProjectId),
                        new NpgsqlParameter("@FileName", cadFile.FileName),
                        new NpgsqlParameter("@FilePath", cadFile.FilePath),
                        new NpgsqlParameter("@UploadedBy", cadFile.UploadedBy),
                        new NpgsqlParameter("@CreatedAt", cadFile.CreatedAt),
                        new NpgsqlParameter("@ModifiedAt", DateTime.UtcNow),
                        new NpgsqlParameter("@ConversionStatus", (object)cadFile.ConversionStatus ?? DBNull.Value));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("A concurrency conflict occurred while updating the database.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the database. See inner exception for details.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred in the repository layer.", ex);
            }
        }
    }
}
