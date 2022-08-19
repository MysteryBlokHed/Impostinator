using System.Collections;

namespace Impostinator
{
    class SavedConfig
    {
        public ArrayList settings;

        public SavedConfig()
        {
            // Initialize data types
            settings = new ArrayList()
            {
                new byte(),
                new int(),
                new byte(),
                new int(),
                new int(),
                new int(),
                new float(),
                new float(),
                new float(),
                new float(),
                new int(),
                new int(),
                new byte(),
                new int(),
                new int(),
                new int(),
                new int(),
                new int(),
                new byte(),
            };
        }
    }
}
