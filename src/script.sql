CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "Chats" (
    "Id" uuid NOT NULL,
    "UserAId" text NOT NULL,
    "UserBId" text NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Chats" PRIMARY KEY ("Id")
);

CREATE TABLE "Frienships" (
    "Id" uuid NOT NULL,
    "UserAId" text NOT NULL,
    "UserBId" text NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Frienships" PRIMARY KEY ("Id")
);

CREATE TABLE "Medias" (
    "Id" uuid NOT NULL,
    "mediaType" integer NOT NULL,
    "Name" text NOT NULL,
    "CreateAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Medias" PRIMARY KEY ("Id")
);

CREATE TABLE "Messages" (
    "Id" uuid NOT NULL,
    "ChatId" uuid NOT NULL,
    "SenderId" text NOT NULL,
    "MessageContent" text NOT NULL,
    "jointMedia" uuid,
    CONSTRAINT "PK_Messages" PRIMARY KEY ("Id")
);

CREATE TABLE "Users" (
    "Id" text NOT NULL,
    "Username" text NOT NULL,
    "ProfilePicture" text,
    "CreateAt" timestamp with time zone NOT NULL,
    "LastSeen" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250122221645_InitDb', '9.0.1');

COMMIT;

