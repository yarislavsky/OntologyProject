using SparqlCaller.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing.Formatting;

namespace SparqlCaller
{
    public static class SparqlQueryBase
    {
        public static List<IEntity> GetEntities<T>(string url, string queryString) where T : IEntity
        {
            return RunQuery(url, queryString)
                .Select(EntityFactory.GetEntity<T>).Where(q => q != null)
                .ToList();
        }

        public static List<SparqlResult> RunQuery(string url, string queryString)
        {

            TripleStore store = new TripleStore();

            //Assume that we fill our Store with data from somewhere

            //Create a dataset for our queries to operate over
            //We need to explicitly state our default graph or the unnamed graph is used
            //Alternatively you can set the second parameter to true to use the union of all graphs
            //as the default graph
            InMemoryDataset ds = new InMemoryDataset(store, new Uri(url));

            //Get the Query processor
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor(ds);

            //Use the SparqlQueryParser to give us a SparqlQuery object
            //Should get a Graph back from a CONSTRUCT query

            SparqlQueryParser sparqlparser = new SparqlQueryParser();
            SparqlQuery query = sparqlparser.ParseFromString(queryString);
            var result = processor.ProcessQuery(query);
            if (result is SparqlResultSet)
            {
                var results = (SparqlResultSet)result;
                return results.ToList();
            }

            //if (result is IGraph)
            //{
            //    //Print out the Results
            //    IGraph g = (IGraph)result;
            //    NTriplesFormatter formatter = new NTriplesFormatter();
            //    return g.Triples.ToList();
            //}

            return null;
        }
    }
}
