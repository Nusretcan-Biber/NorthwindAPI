using Northwind.Utils.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Northwind.Utils.UnitOfWorks
{
    public class UnitOfWork<T> : IDisposable, IUnitOfWork where T : DbContext
    {
        #region Members
        private DbContext _dbContext;

        /// <summary>
        /// hata oluşursa bu listeye doldurulacak
        /// </summary>
        public readonly List<String> ErrorMessageList = new List<String>();



        #endregion

        #region Properties

        /// <summary>
        /// Açılan veri bağlantısı.
        /// </summary>
        private DbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = (DbContext)Activator.CreateInstance(typeof(T));
                }
                return _dbContext;
            }
            set { _dbContext = value; }
        }

        #endregion

        #region Constructre

        /// <summary>
        /// UnitOfWork başlangıcı 
        /// </summary>
        public UnitOfWork()
        {

        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// Repository instance'ı başlatmak için kullanılır
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(DbContext);
        }

        /// <summary>
        /// Değişiklikleri kaydeder ve çıkan hata mesajlarını ErrorMesage'ye kaydeder
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
           int result = -1;
            try
            {
                using (TransactionScope tScope = new TransactionScope())
                {
                    result = DbContext.SaveChanges();
                    tScope.Complete();
                }
            }
            catch (ValidationException ex)
            {
                string errorString = ex.Message;
                ErrorMessageList.Add(errorString);
            }
            catch (DbUpdateException ex)
            {
                string errorString = ex.Message;
                if (ex.InnerException != null)
                {
                    errorString += ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                    {
                        errorString += ex.InnerException.InnerException.Message;
                    }
                }

                ErrorMessageList.Add(errorString);
            }
            catch (Exception ex)
            {
                ErrorMessageList.Add(ex.Message);
            }
            finally
            {
                if (result == -1)
                {
                    Console.WriteLine($"UnitOfWork Save Error. Type : {typeof(T).Name} Error Messages : {JsonConvert.SerializeObject(ErrorMessageList)}");
                }
            }
            return result;
        }

        #endregion
        #region IDispose Members
        public void Dispose()
        {
            DbContext = null;
        }
        #endregion
    }
}
