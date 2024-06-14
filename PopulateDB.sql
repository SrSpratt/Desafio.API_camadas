INSERT INTO ssqldata.dbo.tst_categories (category_id, category_name, category_description)
VALUES
(1, 'Electronics', 'Devices and gadgets'),
(2, 'Books', 'Various genres of books'),
(3, 'Clothing', 'Apparel and accessories');

INSERT INTO ssqldata.dbo.tst_products (Name, Description)
VALUES
('Smartphone', 'A mobile device with various features'),
('Laptop', 'A portable computer'),
('Jeans', 'Denim pants');

INSERT INTO ssqldata.dbo.tst_roles (role_type)
VALUES
('administrator'),
('stockist'),
('vendor');

INSERT INTO ssqldata.dbo.tst_product_category (product_id, category_id)
VALUES
(1, 1),
(2, 1),
(3, 3);

INSERT INTO ssqldata.dbo.tst_stock (Product_id, Amount, Purchase_value, Sale_value, Supplier, Expiration_date)
VALUES
(1, 50, 300.0, 500.0, 'TechSupplier', '2024-12-31'),
(2, 30, 800.0, 1200.0, 'CompSupplier', '2024-12-31'),
(3, 100, 20.0, 40.0, 'ClothSupplier', '2024-12-31');

INSERT INTO ssqldata.dbo.tst_stock_updates (stock_id, operation_type, operation_date, operation_user, operation_amount)
VALUES
(1, 'Create', GETDATE(), 'Fulano', 50),
(2, 'Create', GETDATE(), 'Sicrano', 30),
(3, 'Create', GETDATE(), 'Beltrano', 100),
(1, 'Add', GETDATE(), 'Sicrano', 50),
(1, 'Remove', GETDATE(), 'Sicrano', 50);

INSERT INTO ssqldata.dbo.tst_users (username, user_email, user_password, date_registered, user_role, user_registered)
VALUES
('admin', 'admin@admin.com', 'senha', GETDATE(), 1, 'admin'),
('employee', 'employee@employee.com', 'ahnes', GETDATE(), 2, 'admin'),
('user', 'guest@example.com', 'senah', GETDATE(), 3, 'admin');

INSERT INTO ssqldata.dbo.tst_user_names (user_id, user_name)
VALUES
(1, 'Fulano da Silva'),
(2, 'Sicrano da Selva'),
(3, 'Beltrano Costa');