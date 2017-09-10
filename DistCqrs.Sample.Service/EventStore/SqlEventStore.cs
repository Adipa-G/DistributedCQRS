using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;
using DistCqrs.Core.EventStore;
using DistCqrs.Core.EventStore.Impl;
using Newtonsoft.Json;

namespace DistCqrs.Sample.Service.EventStore
{
    public class SqlEventStore : BaseEventStore
    {
        static SqlEventStore()
        {
            var createTable = @"IF NOT EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='CONTACT' AND XTYPE='U')
                                CREATE TABLE [Contact](
		                            [Id] [bigint] IDENTITY(1,1) NOT NULL,
		                            [RootId] uniqueidentifier NOT NULL,
		                            [EventTimestamp] [timestamp] NOT NULL,
		                            [DATA] [nvarchar](max) NOT NULL
		                            PRIMARY KEY ([Id] ASC)
	                            ) ON [PRIMARY]";

            using (var con = new SqlConnection(Config.ConnectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = createTable;
                cmd.ExecuteNonQuery();
            }
        }

        protected override IEventRecord Create()
        {
            return new EventRecord();
        }

        protected override string Serialize<TRoot>(IEvent<TRoot> evt)
        {
            var settings =
                new JsonSerializerSettings()
                {
                    TypeNameHandling =
                        TypeNameHandling.All
                };
            return JsonConvert.SerializeObject(evt, settings);
        }

        protected override IEvent<TRoot> DeSerialize<TRoot>(string data)
        {
            var settings =
                new JsonSerializerSettings()
                {
                    TypeNameHandling =
                        TypeNameHandling.All
                };
            return JsonConvert.DeserializeObject<IEvent<TRoot>>(data, settings);
        }

        protected override Task Save(IList<IEventRecord> records)
        {
            throw new NotImplementedException();
        }

        protected override Task<IList<IEventRecord>> Load(Guid rootId)
        {
            throw new NotImplementedException();
        }

        public override Task<Type> GetRootType(Guid rootId)
        {
            throw new NotImplementedException();
        }
    }
}
