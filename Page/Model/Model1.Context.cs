//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Page.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ToiletPaperEntities1 : DbContext
    {
        private static ToiletPaperEntities1 _context;
        public ToiletPaperEntities1()
            : base("name=ToiletPaperEntities1")
        {
        }
        public static ToiletPaperEntities1 GetContext()
        {
            if (_context == null)
                    _context = new ToiletPaperEntities1();
            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialProd> MaterialProd { get; set; }
        public DbSet<MaterialsAndProducts> MaterialsAndProducts { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<TypeProd> TypeProd { get; set; }
    }
}
