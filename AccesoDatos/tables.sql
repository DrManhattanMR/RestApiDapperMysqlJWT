-- noinspection SqlNoDataSourceInspectionForFile

create table Roles
(
    id          smallint auto_increment primary key,
    descripcion varchar(20) not null,
    active      tinyint(1) null
);

create table Usuarios
(
    id        int auto_increment primary key,
    nombre    varchar(50) null,
    apellidos varchar(50) null,
    username  varchar(50)  not null,
    direccion varchar(80) null,
    correo    varchar(50) null,
    telefono  varchar(15) null,
    password  varchar(255) not null,
    active    tinyint(1) default 0 not null,
    role      smallint null,
    constraint Usuarios_Roles_id_fk
        foreign key (role) references devnotes.Roles (id)
);