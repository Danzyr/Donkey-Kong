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
                int oldHeight = height;
                Type type = objectType.ElementAt(i);
                EventHandler releasedHandler = null;
                levelObject.Released += releasedHandler = ((sender, e) => {
                levelObject.Released -= releasedHandler;    
                levelObject_Released(sender, e, type, oldHeight); });
                height += levelObject.height + 20;
            }
        }

        private void levelObject_Released(object sender, EventArgs e, Type type, int height)
        {
            LevelObject levelObject = (LevelObject)Activator.CreateInstance(type);
            levelObject.x = 10;
            levelObject.y = height;
            int oldHeight = height;          
            EventHandler releasedHandler = null;
            levelObject.Released += releasedHandler = ((sender, e) => {
                levelObject.Released -= releasedHandler;
                levelObject_Released(sender, e, type, oldHeight);
            });           
        }

        public static IEnumerable<Type> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            List<Type> types = new List<Type>();
            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                types.Add(type);
            }
            return types;
        }
    }
}
