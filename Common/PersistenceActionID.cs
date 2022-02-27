namespace Common
{
    public static class PersistenceActionID
    {
        public static int Delete => (int)PersistenceAction.Delete;
        public static int Insert => (int)PersistenceAction.Insert;

        private enum PersistenceAction
        {
            Delete = -9999,
            Insert = -1
        }
    }
    
    
}
