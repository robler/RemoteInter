using MongoDB.Driver;
using System.Linq.Expressions;
using RemoteInter.Repository.RepositoryBase;

public interface IMongoRepositoryBase<T, TKey>
    where T : IDocument<TKey>
{
    IMongoClient Client { get; }

    IMongoDatabase Database { get; }

    IMongoCollection<T> Collection { get; }

    /// <summary>
    /// 抓取全部資料
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 依據Key抓取一筆資料
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> FindOneAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 依據Filter抓取一筆資料
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> FindOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增單筆資料
    /// </summary>
    /// <param name="document"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task InsertAsync(T document, CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增多筆資料
    /// </summary>
    /// <param name="documents"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task InsertAsync(IEnumerable<T> documents, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新單筆資料
    /// </summary>
    /// <param name="document"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ReplaceAsync(T document, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新多筆資料
    /// </summary>
    /// <param name="documents"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ReplaceAsync(IEnumerable<T> documents, CancellationToken cancellationToken = default);

    /// <summary>
    /// 刪除單筆資料
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 依據filter刪除資料
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// 刪除全部資料
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);
}