-- DROP SCHEMA "KakeysBakery";

CREATE SCHEMA "KakeysBakery"; -- AUTHORIZATION brycecoon_25;
Set search_path to "KakeysBakery";
-- DROP SEQUENCE "KakeysBakery".addon_id_seq;

CREATE SEQUENCE "KakeysBakery".addon_id_seq
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
	NO CYCLE;-- "KakeysBakery".addon definition

-- Drop table

-- DROP TABLE addon;

CREATE TABLE addon (
	id serial4 NOT NULL,
	flavor varchar(70) NULL,
	description varchar(300) NULL,
	suggestedprice money NULL,
	addontypename varchar(60) NULL,
	CONSTRAINT addon_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".basegood definition

-- Drop table

-- DROP TABLE basegood;

CREATE TABLE basegood (
	id serial4 NOT NULL,
	basegoodname varchar(50) NULL,
	flavor varchar(50) NULL,
	suggestedprice money NULL,
	CONSTRAINT basegood_pkey PRIMARY KEY (id)
);


-- "KakeysBakery".customer definition

-- Drop table

-- DROP TABLE customer;

CREATE TABLE customer (
	id serial4 NOT NULL,
	email varchar(50) NOT NULL,
	forename varchar(50) NULL,
	surname varchar(50) NULL,
	phone int4 NULL,
	preferredcontact varchar(30) NULL,
	issubscribed bool NULL,
	CONSTRAINT customer_pkey PRIMARY KEY (id),
	CONSTRAINT customer_preferredcontact_check CHECK (((preferredcontact)::text = ((('Text'::text || 'Call'::text) || 'Email'::text) || 'Other'::text)))
);


-- "KakeysBakery".product definition

-- Drop table

-- DROP TABLE product;

CREATE TABLE product (
	id serial4 NOT NULL,
	basegoodid int4 NULL,
	description varchar(100) NULL,
	productname varchar(50) NULL,
	ispublic bool NULL,
	CONSTRAINT product_pkey PRIMARY KEY (id),
	CONSTRAINT product_basegoodid_fkey FOREIGN KEY (basegoodid) REFERENCES basegood(id)
);


-- "KakeysBakery".product_addon definition

-- Drop table

-- DROP TABLE product_addon;

CREATE TABLE product_addon (
	id serial4 NOT NULL,
	productid int4 NULL,
	addonid int4 NULL,
	CONSTRAINT product_addon_pkey PRIMARY KEY (id),
	CONSTRAINT product_addon_addonid_fkey FOREIGN KEY (addonid) REFERENCES addon(id),
	CONSTRAINT product_addon_productid_fkey FOREIGN KEY (productid) REFERENCES product(id)
);


-- "KakeysBakery".purchase definition

-- Drop table

-- DROP TABLE purchase;

CREATE TABLE purchase (
	id serial4 NOT NULL,
	customerid int4 NULL,
	actualprice money NULL,
	orderdate timestamp NULL,
	specifications varchar(300) NULL,
	isfulfilled bool NULL,
	fulfillmentdate timestamp NULL,
	CONSTRAINT purchase_pkey PRIMARY KEY (id),
	CONSTRAINT purchase_customerid_fkey FOREIGN KEY (customerid) REFERENCES customer(id)
);


-- "KakeysBakery".purchase_product definition

-- Drop table

-- DROP TABLE purchase_product;

CREATE TABLE purchase_product (
	id serial4 NOT NULL,
	purchaseid int4 NULL,
	productid int4 NULL,
	CONSTRAINT purchase_product_pkey PRIMARY KEY (id),
	CONSTRAINT purchase_product_productid_fkey FOREIGN KEY (productid) REFERENCES product(id),
	CONSTRAINT purchase_product_purchaseid_fkey FOREIGN KEY (purchaseid) REFERENCES purchase(id)
);


-- "KakeysBakery".referencephoto definition

-- Drop table

-- DROP TABLE referencephoto;

CREATE TABLE referencephoto (
	id serial4 NOT NULL,
	purchaseid int4 NULL,
	photo bytea NULL,
	CONSTRAINT referencephoto_pkey PRIMARY KEY (id),
	CONSTRAINT referencephoto_purchaseid_fkey FOREIGN KEY (purchaseid) REFERENCES purchase(id)
);


-- "KakeysBakery".cart definition

-- Drop table

-- DROP TABLE cart;

CREATE TABLE cart (
	id serial4 NOT NULL,
	customerid int4 NULL,
	productid int4 NULL,
	CONSTRAINT cart_pkey PRIMARY KEY (id),
	CONSTRAINT cart_customerid_fkey FOREIGN KEY (customerid) REFERENCES customer(id),
	CONSTRAINT cart_productid_fkey FOREIGN KEY (productid) REFERENCES product(id)
);