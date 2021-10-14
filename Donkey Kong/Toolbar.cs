using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Donkey_Kong
{
   public class Toolbar 
    {
        public Toolbar()
        {
            IEnumerable<Type> objectType = GetEnumerableOfType<LevelObject>();
            int height = 15;
            for (int i = 0; i < objectType.Count(); i++)
            {
                LevelObject levelObject = (LevelObject)Activator.CreateInstance(objectType.ElementAt(i));
                levelObject.x = 10;
                levelObject.y = height;
                levelObject.Clicked += new EventHandler((sender, e) => { levelObject_Clicked(sender, e, objectType.ElementAt(i)); });
                height += levelObject.height + 20;
            }
        }

        private void levelObject_Clicked(object sender, EventArgs e, Type type)
        {
            
        }

        public static IEnumerable<Type> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            List<Type> types = new List<Type>();
            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes() .Where(myType => myType.IsClass && !myType.IsSubClass(typeof(T))))
            {
                types.Add(type);
            }
            return types;
        }
    }
}
