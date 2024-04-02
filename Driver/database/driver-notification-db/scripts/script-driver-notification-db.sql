CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

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