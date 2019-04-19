namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminEmployees",
                c => new
                    {
                        AE_id = c.Int(nullable: false, identity: true),
                        AE_name = c.String(),
                        AE_password = c.String(),
                        AE_phone = c.String(),
                        AE_permission = c.String(),
                    })
                .PrimaryKey(t => t.AE_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_id = c.Int(nullable: false, identity: true),
                        User_id = c.Int(nullable: false),
                        Order_status = c.String(),
                        Order_date = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        Payment_method = c.String(),
                        AdminEmployee_AE_id = c.Int(),
                    })
                .PrimaryKey(t => t.Order_id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.AdminEmployees", t => t.AdminEmployee_AE_id)
                .Index(t => t.User_id)
                .Index(t => t.AdminEmployee_AE_id);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderProduct_id = c.Int(nullable: false, identity: true),
                        Order_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                        DeliOrderProduct_number = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderProduct_id)
                .ForeignKey("dbo.Orders", t => t.Order_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .Index(t => t.Order_id)
                .Index(t => t.Product_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Product_id = c.Int(nullable: false, identity: true),
                        Brand_id = c.Int(nullable: false),
                        Cate_id = c.Int(nullable: false),
                        Product_name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date_created = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Detail = c.String(),
                        Image = c.String(),
                        Product_code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Product_id)
                .ForeignKey("dbo.Brands", t => t.Brand_id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Cate_id, cascadeDelete: true)
                .Index(t => t.Brand_id)
                .Index(t => t.Cate_id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Brand_id = c.Int(nullable: false, identity: true),
                        Brand_name = c.String(),
                    })
                .PrimaryKey(t => t.Brand_id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Cate_id = c.Int(nullable: false, identity: true),
                        Cate_name = c.String(),
                    })
                .PrimaryKey(t => t.Cate_id);
            
            CreateTable(
                "dbo.Shippings",
                c => new
                    {
                        Shipping_id = c.Int(nullable: false, identity: true),
                        AE_id = c.Int(nullable: false),
                        Order_id = c.Int(nullable: false),
                        Shipping_start_date = c.DateTime(nullable: false),
                        Shipping_status = c.String(),
                    })
                .PrimaryKey(t => t.Shipping_id)
                .ForeignKey("dbo.AdminEmployees", t => t.AE_id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_id, cascadeDelete: true)
                .Index(t => t.AE_id)
                .Index(t => t.Order_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_id = c.Int(nullable: false, identity: true),
                        User_name = c.String(),
                        User_password = c.String(),
                        User_email = c.String(),
                        User_address = c.String(),
                        User_phone_number = c.String(),
                    })
                .PrimaryKey(t => t.User_id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Cart_id = c.Int(nullable: false, identity: true),
                        User_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                        Product_quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Cart_id)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Product_id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Fb_id = c.Int(nullable: false, identity: true),
                        User_id = c.Int(nullable: false),
                        Fb_title = c.String(),
                        Fb_detail = c.String(),
                    })
                .PrimaryKey(t => t.Fb_id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        Wishlist_id = c.Int(nullable: false, identity: true),
                        User_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Wishlist_id)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "AdminEmployee_AE_id", "dbo.AdminEmployees");
            DropForeignKey("dbo.Wishlists", "User_id", "dbo.Users");
            DropForeignKey("dbo.Wishlists", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Orders", "User_id", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "User_id", "dbo.Users");
            DropForeignKey("dbo.Carts", "User_id", "dbo.Users");
            DropForeignKey("dbo.Carts", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Shippings", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.Shippings", "AE_id", "dbo.AdminEmployees");
            DropForeignKey("dbo.OrderProducts", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Products", "Cate_id", "dbo.Categories");
            DropForeignKey("dbo.Products", "Brand_id", "dbo.Brands");
            DropForeignKey("dbo.OrderProducts", "Order_id", "dbo.Orders");
            DropIndex("dbo.Wishlists", new[] { "Product_id" });
            DropIndex("dbo.Wishlists", new[] { "User_id" });
            DropIndex("dbo.Feedbacks", new[] { "User_id" });
            DropIndex("dbo.Carts", new[] { "Product_id" });
            DropIndex("dbo.Carts", new[] { "User_id" });
            DropIndex("dbo.Shippings", new[] { "Order_id" });
            DropIndex("dbo.Shippings", new[] { "AE_id" });
            DropIndex("dbo.Products", new[] { "Cate_id" });
            DropIndex("dbo.Products", new[] { "Brand_id" });
            DropIndex("dbo.OrderProducts", new[] { "Product_id" });
            DropIndex("dbo.OrderProducts", new[] { "Order_id" });
            DropIndex("dbo.Orders", new[] { "AdminEmployee_AE_id" });
            DropIndex("dbo.Orders", new[] { "User_id" });
            DropTable("dbo.Wishlists");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Carts");
            DropTable("dbo.Users");
            DropTable("dbo.Shippings");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Products");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Orders");
            DropTable("dbo.AdminEmployees");
        }
    }
}
