-- DROP SCHEMA "KakeysBakery";

CREATE SCHEMA "KakeysBakery";
Set search_path to "KakeysBakery";

-- DROP SEQUENCE "KakeysBakery".addon_id_seq;

CREATE SEQUENCE "KakeysBakery".addon_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".base_id_seq;

CREATE SEQUENCE "KakeysBakery".base_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".basegood_id_seq;

CREATE SEQUENCE "KakeysBakery".basegood_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".basegood_size_id_seq;

CREATE SEQUENCE "KakeysBakery".basegood_size_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".cart_id_seq;

CREATE SEQUENCE "KakeysBakery".cart_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".customer_id_seq;

CREATE SEQUENCE "KakeysBakery".customer_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".customer_role_id_seq;

CREATE SEQUENCE "KakeysBakery".customer_role_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".flavor_id_seq;

CREATE SEQUENCE "KakeysBakery".flavor_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".product_addon_id_seq;

CREATE SEQUENCE "KakeysBakery".product_addon_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".product_id_seq;

CREATE SEQUENCE "KakeysBakery".product_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".purchase_id_seq;

CREATE SEQUENCE "KakeysBakery".purchase_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".purchase_product_id_seq;

CREATE SEQUENCE "KakeysBakery".purchase_product_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".referencephoto_id_seq;

CREATE SEQUENCE "KakeysBakery".referencephoto_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;
-- DROP SEQUENCE "KakeysBakery".userrole_id_seq;

CREATE SEQUENCE "KakeysBakery".userrole_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;-- "KakeysBakery".addonflavor definition

-- Drop table

-- DROP TABLE "KakeysBakery".addonflavor;

CREATE TABLE "KakeysBakery".addonflavor (
	id int4 NOT NULL,
	flavor varchar(50) NULL,
	CONSTRAINT addonflavor_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".addontype definition

-- Drop table

-- DROP TABLE "KakeysBakery".addontype;

CREATE TABLE "KakeysBakery".addontype (
	id int4 NOT NULL,
	basetype varchar(50) NULL,
	CONSTRAINT addontype_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".basegood_size definition

-- Drop table

-- DROP TABLE "KakeysBakery".basegood_size;

CREATE TABLE "KakeysBakery".basegood_size (
	id serial4 NOT NULL,
	"size" varchar(50) NULL,
	CONSTRAINT basegood_size_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".basegoodflavor definition

-- Drop table

-- DROP TABLE "KakeysBakery".basegoodflavor;

CREATE TABLE "KakeysBakery".basegoodflavor (
	id int4 DEFAULT nextval('"KakeysBakery".flavor_id_seq'::regclass) NOT NULL,
	flavorname varchar(50) NULL,
	CONSTRAINT flavor_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".basegoodtype definition

-- Drop table

-- DROP TABLE "KakeysBakery".basegoodtype;

CREATE TABLE "KakeysBakery".basegoodtype (
	id int4 DEFAULT nextval('"KakeysBakery".base_id_seq'::regclass) NOT NULL,
	basegood varchar(50) NULL,
	CONSTRAINT base_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".customer definition

-- Drop table

-- DROP TABLE "KakeysBakery".customer;

CREATE TABLE "KakeysBakery".customer (
	id serial4 NOT NULL,
	email varchar(50) NOT NULL,
	forename varchar(50) NULL,
	surname varchar(50) NULL,
	phone varchar(15) NULL,
	preferredcontact varchar(30) NULL,
	issubscribed bool NULL,
	CONSTRAINT customer_pkey PRIMARY KEY (id),
	CONSTRAINT customer_preferredcontact_check CHECK (preferredcontact IN ('Text','Call','Email','Other'))
);


-- "KakeysBakery".product definition

-- Drop table

-- DROP TABLE "KakeysBakery".product;

CREATE TABLE "KakeysBakery".product (
	id serial4 NOT NULL,
	description varchar(100) NULL,
	productname varchar(50) NULL,
	ispublic bool NULL,
	CONSTRAINT product_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".userrole definition

-- Drop table

-- DROP TABLE "KakeysBakery".userrole;

CREATE TABLE "KakeysBakery".userrole (
	id serial4 NOT NULL,
	userrole varchar(20) NOT NULL,
	CONSTRAINT userrole_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".addon definition

-- Drop table

-- DROP TABLE "KakeysBakery".addon;

CREATE TABLE "KakeysBakery".addon (
	id serial4 NOT NULL,
	description varchar(300) NULL,
	suggestedprice money NULL,
	addontypeid int4 NULL,
	addonflavorid int4 NULL,
	CONSTRAINT addon_pkey PRIMARY KEY (id),
	CONSTRAINT addonflavor FOREIGN KEY (addontypeid) REFERENCES "KakeysBakery".addontype(id),
	CONSTRAINT addontype FOREIGN KEY (addonflavorid) REFERENCES "KakeysBakery".addonflavor(id)
);


-- "KakeysBakery".basegood definition

-- Drop table

-- DROP TABLE "KakeysBakery".basegood;

CREATE TABLE "KakeysBakery".basegood (
	id serial4 NOT NULL,
	suggestedprice money NULL,
	pastryid int4 NULL,
	flavorid int4 NULL,
	isavailable bool NULL,
	goodsize int4 NULL,
	CONSTRAINT basegood_pkey PRIMARY KEY (id),
	CONSTRAINT basegood_goodsize_fkey FOREIGN KEY (goodsize) REFERENCES "KakeysBakery".basegood_size(id),
	CONSTRAINT basegoodname FOREIGN KEY (pastryid) REFERENCES "KakeysBakery".basegoodtype(id),
	CONSTRAINT flavorid FOREIGN KEY (flavorid) REFERENCES "KakeysBakery".basegoodflavor(id)
);


-- "KakeysBakery".cart definition

-- Drop table

-- DROP TABLE "KakeysBakery".cart;

CREATE TABLE "KakeysBakery".cart (
	id serial4 NOT NULL,
	customerid int4 NULL,
	productid int4 NULL,
	quantity int4 NULL,
	CONSTRAINT cart_pkey PRIMARY KEY (id),
	CONSTRAINT cart_customerid_fkey FOREIGN KEY (customerid) REFERENCES "KakeysBakery".customer(id),
	CONSTRAINT cart_productid_fkey FOREIGN KEY (productid) REFERENCES "KakeysBakery".product(id)
);


-- "KakeysBakery".customer_role definition

-- Drop table

-- DROP TABLE "KakeysBakery".customer_role;

CREATE TABLE "KakeysBakery".customer_role (
	id serial4 NOT NULL,
	customer_id int4 NULL,
	userrole_id int4 NULL,
	CONSTRAINT customer_role_pkey PRIMARY KEY (id),
	CONSTRAINT customer_role_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES "KakeysBakery".customer(id),
	CONSTRAINT customer_role_userrole_id_fkey FOREIGN KEY (userrole_id) REFERENCES "KakeysBakery".userrole(id)
);


-- "KakeysBakery".product_addon_basegood definition

-- Drop table

-- DROP TABLE "KakeysBakery".product_addon_basegood;

CREATE TABLE "KakeysBakery".product_addon_basegood (
	id int4 DEFAULT nextval('"KakeysBakery".product_addon_id_seq'::regclass) NOT NULL,
	productid int4 NULL,
	addonid int4 NULL,
	basegoodid int4 NULL,
	CONSTRAINT product_addon_pkey PRIMARY KEY (id),
	CONSTRAINT basegood FOREIGN KEY (basegoodid) REFERENCES "KakeysBakery".basegood(id),
	CONSTRAINT product_addon_addonid_fkey FOREIGN KEY (addonid) REFERENCES "KakeysBakery".addon(id),
	CONSTRAINT product_addon_productid_fkey FOREIGN KEY (productid) REFERENCES "KakeysBakery".product(id)
);


-- "KakeysBakery".purchase definition

-- Drop table

-- DROP TABLE "KakeysBakery".purchase;

CREATE TABLE "KakeysBakery".purchase (
	id serial4 NOT NULL,
	customerid int4 NULL,
	actualprice money NULL,
	orderdate timestamp NULL,
	specifications varchar(300) NULL,
	isfulfilled bool NULL,
	fulfillmentdate timestamp NULL,
	CONSTRAINT purchase_pkey PRIMARY KEY (id),
	CONSTRAINT purchase_customerid_fkey FOREIGN KEY (customerid) REFERENCES "KakeysBakery".customer(id)
);


-- "KakeysBakery".purchase_product definition

-- Drop table

-- DROP TABLE "KakeysBakery".purchase_product;

CREATE TABLE "KakeysBakery".purchase_product (
	id serial4 NOT NULL,
	purchaseid int4 NULL,
	productid int4 NULL,
	CONSTRAINT purchase_product_pkey PRIMARY KEY (id),
	CONSTRAINT purchase_product_productid_fkey FOREIGN KEY (productid) REFERENCES "KakeysBakery".product(id),
	CONSTRAINT purchase_product_purchaseid_fkey FOREIGN KEY (purchaseid) REFERENCES "KakeysBakery".purchase(id)
);


-- "KakeysBakery".referencephoto definition

-- Drop table

-- DROP TABLE "KakeysBakery".referencephoto;

CREATE TABLE "KakeysBakery".referencephoto (
	id serial4 NOT NULL,
	purchaseid int4 NULL,
	photo bytea NULL,
	CONSTRAINT referencephoto_pkey PRIMARY KEY (id),
	CONSTRAINT referencephoto_purchaseid_fkey FOREIGN KEY (purchaseid) REFERENCES "KakeysBakery".purchase(id)
);