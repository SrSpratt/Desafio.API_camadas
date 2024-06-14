CREATE DATABASE ssqldata;

CREATE TABLE ssqldata.dbo.tst_categories (
	category_id int IDENTITY(1,1) NOT NULL,
	category_name varchar(100) COLLATE Modern_Spanish_CI_AS NOT NULL,
	category_description text COLLATE Modern_Spanish_CI_AS NULL,
	CONSTRAINT PK__tst_cate__D54EE9B4B4F6DFE0 PRIMARY KEY (category_id)
);

CREATE TABLE ssqldata.dbo.tst_products (
	Code int IDENTITY(1,1) NOT NULL,
	Name varchar(100) COLLATE Modern_Spanish_CI_AS NOT NULL,
	Description text COLLATE Modern_Spanish_CI_AS NOT NULL,
	CONSTRAINT PK__tst_prod__A25C5AA6F2724DC4 PRIMARY KEY (Code),
	CONSTRAINT name_unique UNIQUE (Name)
);

CREATE TABLE ssqldata.dbo.tst_roles (
	role_id int IDENTITY(1,1) NOT NULL,
	role_type varchar(15) COLLATE Modern_Spanish_CI_AS NULL,
	CONSTRAINT PK__tst_role__760965CC3EAA61B1 PRIMARY KEY (role_id)
);

CREATE TABLE ssqldata.dbo.tst_product_category (
	product_id int NOT NULL,
	category_id int NOT NULL,
	CONSTRAINT PK__tst_prod__1A56936E0667DFA1 PRIMARY KEY (product_id,category_id),
	CONSTRAINT FK_categoryproduct FOREIGN KEY (product_id) REFERENCES tst_products(Code),
	CONSTRAINT FK_productcategory FOREIGN KEY (category_id) REFERENCES tst_categories(category_id)
);

CREATE TABLE ssqldata.dbo.tst_stock (
	Product_id int NOT NULL,
	Amount int NOT NULL,
	Purchase_value real NOT NULL,
	Sale_value real NOT NULL,
	Stock_id int IDENTITY(1,1) NOT NULL,
	Supplier varchar(50) COLLATE Modern_Spanish_CI_AS NULL,
	Expiration_date datetime NULL,
	CONSTRAINT PK__tst_stoc__EF9B7AD0C0784BC8 PRIMARY KEY (Stock_id),
	CONSTRAINT product_id_unique UNIQUE (Product_id),
	CONSTRAINT FK__tst_stock__Produ__398D8EEE FOREIGN KEY (Product_id) REFERENCES tst_products(Code)
);

CREATE TABLE ssqldata.dbo.tst_stock_updates (
	operation_id int IDENTITY(1,1) NOT NULL,
	stock_id int NOT NULL,
	operation_type varchar(10) COLLATE Modern_Spanish_CI_AS NOT NULL,
	operation_date datetime NOT NULL,
	operation_user varchar(255) COLLATE Modern_Spanish_CI_AS NOT NULL,
	operation_amount int NOT NULL,
	CONSTRAINT PK__tst_stoc__A36717A5F227025E PRIMARY KEY (operation_id,stock_id),
	CONSTRAINT UQ__tst_stoc__9DE17122AA83B808 UNIQUE (operation_id),
	CONSTRAINT FK__tst_stock__stock__05D8E0BE FOREIGN KEY (stock_id) REFERENCES tst_stock(Stock_id)
);

CREATE TABLE ssqldata.dbo.tst_users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	user_email varchar(50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	user_password varchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	date_registered datetime NOT NULL,
	user_role int NOT NULL,
	user_registered varchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	CONSTRAINT PK__tst_user__B9BE370F7A66BDE2 PRIMARY KEY (user_id),
	CONSTRAINT UQ__tst_user__B0FBA2126E73D96D UNIQUE (user_email),
	CONSTRAINT UQ__tst_user__F3DBC572CF2DE7D4 UNIQUE (username),
	CONSTRAINT FK__tst_users__user___74AE54BC FOREIGN KEY (user_role) REFERENCES tst_roles(role_id)
);

CREATE TABLE ssqldata.dbo.tst_user_names (
	user_name_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	user_name varchar(50) COLLATE Modern_Spanish_CI_AS NOT NULL,
	CONSTRAINT PK__tst_user__3E9BA7EC0F72560D PRIMARY KEY (user_id,user_name_id),
	CONSTRAINT UQ__tst_user__72590E3824CB29F7 UNIQUE (user_name_id),
	CONSTRAINT FK__tst_user___user___0A9D95DB FOREIGN KEY (user_id) REFERENCES tst_users(user_id)
);