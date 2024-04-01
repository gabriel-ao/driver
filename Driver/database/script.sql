CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "CnhTypes" (
    "ID" UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    "Description" VARCHAR(100),
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL
);

INSERT INTO "CnhTypes" ("ID", "Description")
VALUES 
    ('fd70e3c2-783d-4a1e-b267-3b2b485c3baf', 'A'),
    ('89a4c943-5a07-45a9-a3bf-b1c70cfd8fc4', 'B'),
    ('e17686a6-d1f4-4c37-b79e-34835b475eda', 'A + B');



CREATE TABLE "Plans" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Days" INT,
    "Price" DECIMAL(10,2),
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL
);

INSERT INTO "Plans" ("ID", "Days", "Price")
VALUES 
    ('6e731b7e-5b9b-4d25-8d62-787d67d4a064', 7, 30.00),
    ('9a2d1a90-7f88-4877-81d8-5304b6f1a71c', 15, 28.00),
    ('c7af6d07-ec99-49f8-a0c6-646f04430d88', 30, 22.00);


CREATE TABLE "ProcessStatus" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Description" VARCHAR(100),
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL
);

INSERT INTO "ProcessStatus" ("ID","Description")
VALUES 
    ('7292e427-226c-4195-ad0d-c4e4f91dc987', 'Disponível'),
    ('621a379a-cd1a-43c9-b11a-1d5cee353298', 'Aceito'),
    ('dde1525d-cb05-4840-a214-af69005ada0f', 'Entregue');



CREATE TABLE "Vehicles" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Year" INT,
    "Model" VARCHAR(100),
    "Plate" VARCHAR(20) UNIQUE,
    "Active" BOOLEAN,
    "Deleted" BOOLEAN,
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL
);



CREATE TABLE "ManagerUsers" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Email" VARCHAR(255),
    "Password" VARCHAR(255),
    "Active" BOOLEAN,
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL
);


CREATE TABLE "Drivers" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "FirstName" VARCHAR(100) NOT NULL,
    "LastName" VARCHAR(100) NOT NULL,
    "CNPJ" VARCHAR(20) UNIQUE,
    "BirthDate" DATE NOT NULL,
    "CnhNumber" VARCHAR(20)  NOT NULL UNIQUE,
    "CnhID" UUID NOT NULL,
    "CnhImage" VARCHAR(1000) NOT NULL,
    "Password" VARCHAR(255)  NOT NULL,
    "Active" BOOLEAN,
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL,
    CONSTRAINT fk_cnh FOREIGN KEY ("CnhID") REFERENCES "CnhTypes"("ID")
);


CREATE TABLE "Rents" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "VehicleID" UUID NOT NULL,
    "PlanID" UUID NOT NULL,
    "DriverID" UUID NOT NULL,
    "InitialRent" TIMESTAMP,
    "PreviousRent" TIMESTAMP,
    "FinishRent" TIMESTAMP,
    "PriceRent" DECIMAL(10,2),
    "PricePaid" DECIMAL(10,2),
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL,
    CONSTRAINT "fk_vehicle" FOREIGN KEY ("VehicleID") REFERENCES "Vehicles"("ID"),
    CONSTRAINT "fk_plan" FOREIGN KEY ("PlanID") REFERENCES "Plans"("ID"),
    CONSTRAINT "fk_driver" FOREIGN KEY ("DriverID") REFERENCES "Drivers"("ID")
);

CREATE TABLE "Delivery_Orders" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
	"Title" VARCHAR(100),
    "Description" VARCHAR(500),
    "StatusID" UUID NOT NULL,
    "Price" DECIMAL(10,2),
    "RentID" UUID NOT NULL,
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT "fk_status" FOREIGN KEY ("StatusID") REFERENCES "ProcessStatus"("ID"),
    CONSTRAINT "fk_rent" FOREIGN KEY ("RentID") REFERENCES "Rents"("ID")
);

CREATE TABLE "Notifications" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
	"Title" VARCHAR(100),
    "Description" VARCHAR(255),
    "OrderID" UUID,
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL,
    CONSTRAINT "fk_delivery_order" FOREIGN KEY ("OrderID") REFERENCES "Delivery_Orders"("ID")
);

CREATE TABLE "Drivers_Notifications" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "DriverID" UUID NOT NULL,
    "NotificationID" UUID NOT NULL,
    "Read" BOOLEAN,
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdateDate" TIMESTAMP DEFAULT NULL,
    CONSTRAINT "fk_driver" FOREIGN KEY ("DriverID") REFERENCES "Drivers"("ID"),
    CONSTRAINT "fk_notification" FOREIGN KEY ("NotificationID") REFERENCES "Notifications"("ID")
);


CREATE TABLE "Logs" (
    "ID" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "MethodName" VARCHAR(500),
    "Message" VARCHAR(1000),
    "StackMessage" VARCHAR(1000),
    "Type" VARCHAR(100),
    "CreateDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


