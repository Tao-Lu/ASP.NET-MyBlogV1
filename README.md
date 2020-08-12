# MyBlogV1

## Structure

### Model

#### MyBlogV1Model

* DbContext: MyBlogV1DBEntities
* Article
* ArticleCategoryInt
* Category
* Comment
* Favorite
* Following
* Reply
* Role
* User
* UserRoleInt

### IDAL

* IBaseDAL<T> where T: class, new()
  * `void AddEntity(T entity);`
  * `void DeleteEntity(T entity);`
  * `void EditEntity(T entity);`
  * `IQueryable<T> GetEntities(Expression<Func<T, bool>> whereExpression);`
  * `T GetEntity(Expression<Func<T, bool>> firstOrDefaultExpression);`
  * `IQueryable<T> GetEntitiesByPageOrdered<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereExpression, Expression<Func<T, s>> orderByExpression, bool isAsc);`
* Ten Interfaces inherit IBaseDAL<T>
  * IArticleDAL
  * IArticleCategoryIntDAL
  * ICategoryDAL
  * ICommentDAL
  * IFavoriteDAL
  * IFollowingDAL
  * IReplyDAL
  * IRoleDAL
  * IUserDAL
  * IUserRoleIntDAL
* 



### DAL

* DbContextFactory: 

  * Singleton, 

  * Ensure there is only one dbcontext, so changes made and saveChanges() both are on same dbcontext

  * ```c#
    private static DbContext _dbContext;
    
    public static DbContext CreateDbContext()
    {
    	_dbContext = (DbContext) CallContext.GetData("DbContext");
    	if(_dbContext == null)
    	{
    		_dbContext = new MyBlogV1DBEntities();
    		CallContext.SetData("DbContext", _dbContext);
    	}
                
    	return _dbContext;
    }
    ```

    

* BaseDAL<T> : IBaseDAL<T> where T : class, new()

  * ```c#
    // get dbContext from DbContextFactory
    public DbContext dbContext = DbContextFactory.CreateDbContext();
    ```

  * ```C#
    public void AddEntity(T entity)
    {
    	// dbContext.Set<T>().Add(entity);
    	dbContext.Entry<T>(entity).State = EntityState.Added;
    }
    
    public void DeleteEntity(T entity)
    {
    	// dbContext.Set<T>().Remove(entity);
    	dbContext.Entry<T>(entity).State = EntityState.Deleted;
    }
    
    public void EditEntity(T entity)
    {
    	dbContext.Entry<T>(entity).State = EntityState.Modified;
    }
    
    public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereExpression)
    {
    	return dbContext.Set<T>().Where<T>(whereExpression);
    }
    
    // checks result is null or not
    public T GetEntity(Expression<Func<T, bool>> firstOrDefaultExpression)
    {
    	return dbContext.Set<T>().FirstOrDefault<T>(firstOrDefaultExpression);
    }
    
    public IQueryable<T> GetEntitiesByPageOrdered<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereExpression, Expression<Func<T, s>> orderByExpression, bool isAsc)
    {
    	var entitiesByPage = dbContext.Set<T>().Where<T>(whereExpression);
    	totalCount = entitiesByPage.Count();
    
    	if (isAsc)
    	{
    		entitiesByPage = entitiesByPage.OrderBy<T, s>(orderByExpression).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
    	}
    	else
    	{
    		entitiesByPage = entitiesByPage.OrderByDescending<T, s>(orderByExpression).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
    	}
    
    	return entitiesByPage;
    }
    ```

    

* Ten Classes inherit BaseDAL<T> and implement corresponding interface

  * ArticleDAL
  * ArticleCategoryIntDAL
  * CategoryDAL
  * CommentDAL
  * FavoriteDAL
  * FollowingDAL
  * ReplyDAL
  * RoleDAL
  * UserDAL
  * UserRoleIntDAL

### DBSession

* DbSession class

* DbSessionFactory

* AbstractFactory

* 

  ```c#
    <!-- copy and paste from Model.App.Config -->
    <configSections>
      <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
      <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
    </configSections>
    <connectionStrings>
      <add name="MyBlogV1DBEntities" connectionString="metadata=res://*/MyBlogV1Model.csdl|res://*/MyBlogV1Model.ssdl|res://*/MyBlogV1Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.;initial catalog=MyBlogV1DB;user id=sa;password=sa;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
    </connectionStrings>
    <entityFramework>
      <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
        <parameters>
          <parameter value="mssqllocaldb" />
        </parameters>
      </defaultConnectionFactory>
      <providers>
        <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
      </providers>
    </entityFramework>
    <!-- end here -->
  ```

* ```c#
  // configuration of AbstracyFactory
  <!-- configuration of AbstractFactory  -->
      <add key="AssemblyPath" value="DAL"/>
  ```

* 

### IBLL



### BLL



### WebUI

#### Site Search

[Lucene.Net](https://lucenenet.apache.org/)



####  job scheduling system

[Quartz.NET](https://www.quartz-scheduler.net/)



#### Logging Services

[Log4Net](https://logging.apache.org/log4net/)