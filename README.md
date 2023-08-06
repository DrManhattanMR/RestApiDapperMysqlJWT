# RestApiDapperMysqlJWT
boilerplate mysql rest api con Dapper para el login de usuarios, incluye tabla usuario y roles (CRUD) e integracion de JWT authorization

1.- en ServiciosBD agregar tus datos de conexion a mysql
2.- posteriormente ejecuta las sentencias sql del archivo tables.sql 
3.- el endpoint login, puedes iniciar sesion con el metodo post enviando una entidad tipo Usuario, toma en cuenta que unicamente los parametros username y password son requeridos
    siendo estos username="manucho" y password="admin";
4.- el endpoint rol en su metodo get requiere que se envie el token de autorizacion previamente devuelto en la solicitud del paso 3.

enjoy.
