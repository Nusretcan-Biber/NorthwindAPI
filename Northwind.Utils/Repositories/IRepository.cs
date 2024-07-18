using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Utils.Repositories
{
    public interface IRepository<TModel> where TModel : class
    {

        /// <summary>
        /// Tüm veriyi getir
        /// </summary>
        /// <returns></returns>
        IQueryable<TModel> GetAll();

        /// <summary>
        /// Veriyi where sorgusuna göre getirir
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TModel> GetAll(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Veriyi single olarak getirir
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TModel Get(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Veritabanına veri ekler
        /// </summary>
        /// <param name="model"></param>
        void Add(TModel model);

        /// <summary>
        /// Veritabanına veriyi listeleyerek ekler istenilen add işlemlerini listeleyerek galiba sormam lazım
        /// </summary>
        /// <param name="model"></param>
        void AddRange(List<TModel> model);

        /// <summary>
        /// Veriyi günceller
        /// </summary>
        /// <param name="model"></param>
        void Update(TModel model);

        /// <summary>
        /// Verilen modeli siler
        /// </summary>
        /// <param name="model"></param>
        /// <param name="forceDelete"></param>
        void Delete(TModel model, bool forceDelete = false);

        /// <summary>
        /// Predicate göre veriyi siler
        /// </summary>
        /// <param name="model"></param>
        void Delete(Expression<Func<TModel,bool>> predicate, bool forceDelete = false);

        /// <summary>
        /// Verinin mevcut olup olmama durumunu kontrol eder true yada false döner
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// DbContext'i getirir
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();

        TModel GetByIdiki(short id);
    }
}
