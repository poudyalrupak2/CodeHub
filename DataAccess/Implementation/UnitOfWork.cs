using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
   public class UnitOfWork:IDisposable
    {
        private CodeDbContext context = new CodeDbContext();
        private GenericRepository<InpCodes> InpCodes;
        private GenericRepository<InpCodesImages> InpCodesImages;
        private GenericRepository<InpWorks> InpWorks;
      

        public GenericRepository<InpCodes> InpcodeRepository
        {
            get
            {

                if (this.InpCodes == null)
                {
                    this.InpCodes = new GenericRepository<InpCodes>(context);
                }
                return InpCodes;
            }
        }
        public GenericRepository<InpCodesImages> InpImageRepository
        {
            get
            {

                if (this.InpCodesImages == null)
                {
                    this.InpCodesImages = new GenericRepository<InpCodesImages>(context);
                }
                return InpCodesImages;
            }
        }
        public GenericRepository<InpWorks> InpWorksRepository
        {
            get
            {

                if (this.InpWorks == null)
                {
                    this.InpWorks = new GenericRepository<InpWorks>(context);
                }
                return InpWorks;
            }
        }
        public void Save()
        {
            using (var dbContextTransaction =context.Database.BeginTransaction())
            {
                try
                {
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    dbContextTransaction.Rollback();
                }
            }

            
        }
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                context = null;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }

    }
}
