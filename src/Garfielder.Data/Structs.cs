


using System;
using SubSonic.Schema;
using System.Collections.Generic;
using SubSonic.DataProviders;
using System.Data;

namespace Garfielder.Data {
	
        /// <summary>
        /// Table: User
        /// Primary Key: Id
        /// </summary>

        public class UserTable: DatabaseTable {
            
            public UserTable(IDataProvider provider):base("User",provider){
                ClassName = "User";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Nickname", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Email", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Password", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 12
                });

                Columns.Add(new DatabaseColumn("CreatedBy", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatedAt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Nickname{
                get{
                    return this.GetColumn("Nickname");
                }
            }
				
   			public static string NicknameColumn{
			      get{
        			return "Nickname";
      			}
		    }
            
            public IColumn Email{
                get{
                    return this.GetColumn("Email");
                }
            }
				
   			public static string EmailColumn{
			      get{
        			return "Email";
      			}
		    }
            
            public IColumn Password{
                get{
                    return this.GetColumn("Password");
                }
            }
				
   			public static string PasswordColumn{
			      get{
        			return "Password";
      			}
		    }
            
            public IColumn CreatedBy{
                get{
                    return this.GetColumn("CreatedBy");
                }
            }
				
   			public static string CreatedByColumn{
			      get{
        			return "CreatedBy";
      			}
		    }
            
            public IColumn CreatedAt{
                get{
                    return this.GetColumn("CreatedAt");
                }
            }
				
   			public static string CreatedAtColumn{
			      get{
        			return "CreatedAt";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: XFile
        /// Primary Key: Id
        /// </summary>

        public class XFileTable: DatabaseTable {
            
            public XFileTable(IDataProvider provider):base("XFile",provider){
                ClassName = "XFile";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Extension", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 600
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreatedAt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Extension{
                get{
                    return this.GetColumn("Extension");
                }
            }
				
   			public static string ExtensionColumn{
			      get{
        			return "Extension";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn CreatedAt{
                get{
                    return this.GetColumn("CreatedAt");
                }
            }
				
   			public static string CreatedAtColumn{
			      get{
        			return "CreatedAt";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: TopicFile
        /// Primary Key: FileID
        /// </summary>

        public class TopicFileTable: DatabaseTable {
            
            public TopicFileTable(IDataProvider provider):base("TopicFile",provider){
                ClassName = "TopicFile";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("TopicID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FileID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn TopicID{
                get{
                    return this.GetColumn("TopicID");
                }
            }
				
   			public static string TopicIDColumn{
			      get{
        			return "TopicID";
      			}
		    }
            
            public IColumn FileID{
                get{
                    return this.GetColumn("FileID");
                }
            }
				
   			public static string FileIDColumn{
			      get{
        			return "FileID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: TopicTag
        /// Primary Key: TagID
        /// </summary>

        public class TopicTagTable: DatabaseTable {
            
            public TopicTagTable(IDataProvider provider):base("TopicTag",provider){
                ClassName = "TopicTag";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("TopicID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TagID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn TopicID{
                get{
                    return this.GetColumn("TopicID");
                }
            }
				
   			public static string TopicIDColumn{
			      get{
        			return "TopicID";
      			}
		    }
            
            public IColumn TagID{
                get{
                    return this.GetColumn("TagID");
                }
            }
				
   			public static string TagIDColumn{
			      get{
        			return "TagID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: UserTag
        /// Primary Key: TagID
        /// </summary>

        public class UserTagTable: DatabaseTable {
            
            public UserTagTable(IDataProvider provider):base("UserTag",provider){
                ClassName = "UserTag";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TagID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn TagID{
                get{
                    return this.GetColumn("TagID");
                }
            }
				
   			public static string TagIDColumn{
			      get{
        			return "TagID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: TopicGroup
        /// Primary Key: GroupID
        /// </summary>

        public class TopicGroupTable: DatabaseTable {
            
            public TopicGroupTable(IDataProvider provider):base("TopicGroup",provider){
                ClassName = "TopicGroup";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("TopicID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("GroupID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn TopicID{
                get{
                    return this.GetColumn("TopicID");
                }
            }
				
   			public static string TopicIDColumn{
			      get{
        			return "TopicID";
      			}
		    }
            
            public IColumn GroupID{
                get{
                    return this.GetColumn("GroupID");
                }
            }
				
   			public static string GroupIDColumn{
			      get{
        			return "GroupID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: TopicComment
        /// Primary Key: Id
        /// </summary>

        public class TopicCommentTable: DatabaseTable {
            
            public TopicCommentTable(IDataProvider provider):base("TopicComment",provider){
                ClassName = "TopicComment";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TopicID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 600
                });

                Columns.Add(new DatabaseColumn("CreatedBy", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatedAt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
            
            public IColumn TopicID{
                get{
                    return this.GetColumn("TopicID");
                }
            }
				
   			public static string TopicIDColumn{
			      get{
        			return "TopicID";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
            
            public IColumn CreatedBy{
                get{
                    return this.GetColumn("CreatedBy");
                }
            }
				
   			public static string CreatedByColumn{
			      get{
        			return "CreatedBy";
      			}
		    }
            
            public IColumn CreatedAt{
                get{
                    return this.GetColumn("CreatedAt");
                }
            }
				
   			public static string CreatedAtColumn{
			      get{
        			return "CreatedAt";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: EntityLog
        /// Primary Key: Id
        /// </summary>

        public class EntityLogTable: DatabaseTable {
            
            public EntityLogTable(IDataProvider provider):base("EntityLog",provider){
                ClassName = "EntityLog";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EntityName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("EntityID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Operation", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreatedAt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
            
            public IColumn EntityName{
                get{
                    return this.GetColumn("EntityName");
                }
            }
				
   			public static string EntityNameColumn{
			      get{
        			return "EntityName";
      			}
		    }
            
            public IColumn EntityID{
                get{
                    return this.GetColumn("EntityID");
                }
            }
				
   			public static string EntityIDColumn{
			      get{
        			return "EntityID";
      			}
		    }
            
            public IColumn Operation{
                get{
                    return this.GetColumn("Operation");
                }
            }
				
   			public static string OperationColumn{
			      get{
        			return "Operation";
      			}
		    }
            
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn CreatedAt{
                get{
                    return this.GetColumn("CreatedAt");
                }
            }
				
   			public static string CreatedAtColumn{
			      get{
        			return "CreatedAt";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Tag
        /// Primary Key: Id
        /// </summary>

        public class TagTable: DatabaseTable {
            
            public TagTable(IDataProvider provider):base("Tag",provider){
                ClassName = "Tag";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Slug", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatedBy", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatedAt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Slug{
                get{
                    return this.GetColumn("Slug");
                }
            }
				
   			public static string SlugColumn{
			      get{
        			return "Slug";
      			}
		    }
            
            public IColumn CreatedBy{
                get{
                    return this.GetColumn("CreatedBy");
                }
            }
				
   			public static string CreatedByColumn{
			      get{
        			return "CreatedBy";
      			}
		    }
            
            public IColumn CreatedAt{
                get{
                    return this.GetColumn("CreatedAt");
                }
            }
				
   			public static string CreatedAtColumn{
			      get{
        			return "CreatedAt";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Topic
        /// Primary Key: Id
        /// </summary>

        public class TopicTable: DatabaseTable {
            
            public TopicTable(IDataProvider provider):base("Topic",provider){
                ClassName = "Topic";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200
                });

                Columns.Add(new DatabaseColumn("Slug", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Url", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 600
                });

                Columns.Add(new DatabaseColumn("ContentX", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Grade", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Double,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreatedAt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Slug{
                get{
                    return this.GetColumn("Slug");
                }
            }
				
   			public static string SlugColumn{
			      get{
        			return "Slug";
      			}
		    }
            
            public IColumn Url{
                get{
                    return this.GetColumn("Url");
                }
            }
				
   			public static string UrlColumn{
			      get{
        			return "Url";
      			}
		    }
            
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
            
            public IColumn ContentX{
                get{
                    return this.GetColumn("ContentX");
                }
            }
				
   			public static string ContentXColumn{
			      get{
        			return "ContentX";
      			}
		    }
            
            public IColumn Grade{
                get{
                    return this.GetColumn("Grade");
                }
            }
				
   			public static string GradeColumn{
			      get{
        			return "Grade";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn CreatedAt{
                get{
                    return this.GetColumn("CreatedAt");
                }
            }
				
   			public static string CreatedAtColumn{
			      get{
        			return "CreatedAt";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Group
        /// Primary Key: Id
        /// </summary>

        public class GroupTable: DatabaseTable {
            
            public GroupTable(IDataProvider provider):base("Group",provider){
                ClassName = "Group";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Slug", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 600
                });

                Columns.Add(new DatabaseColumn("Level", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ParentID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreatedBy", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatedAt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Slug{
                get{
                    return this.GetColumn("Slug");
                }
            }
				
   			public static string SlugColumn{
			      get{
        			return "Slug";
      			}
		    }
            
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
            
            public IColumn Level{
                get{
                    return this.GetColumn("Level");
                }
            }
				
   			public static string LevelColumn{
			      get{
        			return "Level";
      			}
		    }
            
            public IColumn ParentID{
                get{
                    return this.GetColumn("ParentID");
                }
            }
				
   			public static string ParentIDColumn{
			      get{
        			return "ParentID";
      			}
		    }
            
            public IColumn CreatedBy{
                get{
                    return this.GetColumn("CreatedBy");
                }
            }
				
   			public static string CreatedByColumn{
			      get{
        			return "CreatedBy";
      			}
		    }
            
            public IColumn CreatedAt{
                get{
                    return this.GetColumn("CreatedAt");
                }
            }
				
   			public static string CreatedAtColumn{
			      get{
        			return "CreatedAt";
      			}
		    }
            
                    
        }
        
}