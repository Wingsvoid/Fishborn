using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fishborn.Class
{
    class Storage
    {
        private List<Generation> generations;

        public Storage()
        {
            generations = new List<Generation>();
        }
        public Storage(List<Generation> generations)
        {
            this.generations = generations;
        }
        public Storage(string json)
        {
            this.generations = Deserialize(json);
        }

        public string Serialize()
        {
            string json = "";
            json = JsonConvert.SerializeObject(Generations);
            return json;
        }

        public List<Generation> Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<List<Generation>>(json);
        }

        internal List<Generation> Generations { get => generations; set => generations = value; }
    }
}
