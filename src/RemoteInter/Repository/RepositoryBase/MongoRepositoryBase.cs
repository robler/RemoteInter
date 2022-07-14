using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RemoteInter.Repository.RepositoryBase.Attributes;
using System.Linq.Expressions;

namespace RemoteInter.Repository.RepositoryBase;

public class MongoRepositoryBase<T, TKey> : IMongoRepositoryBase<T, TKey>
    where T : IDocument<TKey>
{
    public IMongoClient Client { get; }

    public IMongoDatabase Database { get; }

    public IMongoCollection<T> Collection { get; }

    public MongoRepositoryBase(IOptions<List<MongoRepositoryOption>> option)
    {
        var _tableName = (typeof(T).GetCustomAttributes(typeof(MongoCollectionAttribute), false).FirstOrDefault() as MongoCollectionAttribute).Name;
        var _connectionName = (typeof(T).GetCustomAttributes(typeof(MongoConnectionAttribute), false).FirstOrDefault() as MongoConnectionAttribute)?.Name;

        var _option = _connectionName != null
            ? option.Value.First(i => i.ConnectionName.Equals(_connectionName)) ?? option.Value.First()
            : option.Value.First();

        var mongoUrl = new MongoUrl(_option.ConnectionString);
        Client = new MongoClient(mongoUrl);

        Database = Client.GetDatabase(mongoUrl.DatabaseName);
        Collection = Database.GetCollection<T>(_tableName);
    }

    #region public

    /// <summary>
    /// 抓取全部資料
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
        => (await Collection.FindAsync(new BsonDocument())).ToEnumerable(cancellationToken);

    /// <summary>
    /// 依據Key抓取一筆資料
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<T> FindOneAsync(TKey id, CancellationToken cancellationToken = default)
        => (await Collection.FindAsync(Builders<T>.Filter.Eq("_id", id))).FirstOrDefault(cancellationToken);

    /// <summary>
    /// 依據filter抓取一筆資料
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        => await Collection.Find(Builders<T>.Filter.Where(filter)).FirstOrDefaultAsync(cancellationToken);

    /// <summary>
    /// 新增單筆資料
    /// </summary>
    /// <param name="document"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task InsertAsync(T document, CancellationToken cancellationToken = default)
        => await Collection.InsertOneAsync(document, cancellationToken: cancellationToken);

    /// <summary>
    /// 新增多筆資料
    /// </summary>
    /// <param name="documents"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task InsertAsync(IEnumerable<T> documents, CancellationToken cancellationToken = default)
        => await Collection.BulkWriteAsync(documents.Select(document => new InsertOneModel<T>(document)), cancellationToken: cancellationToken);

    /// <summary>
    /// 更新單筆資料
    /// </summary>
    /// <param name="document"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> ReplaceAsync(T document, CancellationToken cancellationToken = default)
        => (await Collection.ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, document.Id), document, new ReplaceOptions { IsUpsert = true })).IsAcknowledged;

    /// <summary>
    /// 更新多筆資料
    /// </summary>
    /// <param name="documents"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> ReplaceAsync(IEnumerable<T> documents, CancellationToken cancellationToken = default)
        => (
                await Collection.BulkWriteAsync(
                    documents.Select(document => new ReplaceOneModel<T>(Builders<T>.Filter.Eq(x => x.Id, document.Id), document) { IsUpsert = true }),
                    cancellationToken: cancellationToken
                )
            )
            .IsAcknowledged;

    /// <summary>
    /// 刪除單筆資料
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        => (await Collection.DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id), cancellationToken)).IsAcknowledged;

    /// <summary>
    /// 依據filter刪除資料
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        => (await Collection.DeleteManyAsync(filter, cancellationToken)).IsAcknowledged;

    /// <summary>
    /// 刪除全部資料
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        => (await Collection.DeleteManyAsync(new BsonDocument(), cancellationToken)).IsAcknowledged;

    #endregion public

    #region protected

    /// <summary>
    /// 依據filter抓取單筆資料
    /// </summary>
    /// <param name="filter">篩選條件</param>
    /// <param name="options">特殊條件</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async Task<T> FindOneAsync(FilterDefinition<T> filter, FindOptions<T> options = null, CancellationToken cancellationToken = default)
        => (await Collection.FindAsync(filter, options, cancellationToken)).FirstOrDefault(cancellationToken);

    /// <summary>
    /// 依據filter抓取資料
    /// </summary>
    /// <param name="filter">篩選條件</param>
    /// <param name="options">特殊條件</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T> options = null, CancellationToken cancellationToken = default)
        => (await Collection.FindAsync(filter, options, cancellationToken)).ToEnumerable(cancellationToken);

    #endregion protected
}