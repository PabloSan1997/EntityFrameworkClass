using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace entity.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "character varying(1500)",
                maxLength: 1500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1500)",
                oldMaxLength: 1500);

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("105dc87d-cbfa-4b09-875a-8c80e707a771"), "Campo para saber que hay", "A la madre", 22 },
                    { new Guid("5c317d28-3d67-4979-84e2-993be760edeb"), "Actividades importantes de hacer", "Actividades Pendientes", 220 },
                    { new Guid("69dd5d86-01db-467e-8bc2-01c24f956aa5"), "Actividades que tienen que ver más con la persona y el oseo", "Actividades Personales", 50 }
                });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("fe2de405-c38e-4c90-ac52-da0540dfb410"), new Guid("5c317d28-3d67-4979-84e2-993be760edeb"), null, new DateTime(2023, 10, 9, 20, 21, 39, 512, DateTimeKind.Local).AddTicks(5634), 1, "Pago de servicios publicos" },
                    { new Guid("fe2de405-c38e-4c90-ac52-da0540dfb411"), new Guid("5c317d28-3d67-4979-84e2-993be760edeb"), null, new DateTime(2023, 10, 9, 20, 21, 39, 512, DateTimeKind.Local).AddTicks(5652), 0, "Terminar de ver pelicula en netflix" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("105dc87d-cbfa-4b09-875a-8c80e707a771"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("69dd5d86-01db-467e-8bc2-01c24f956aa5"));

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "TareaId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb410"));

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "TareaId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb411"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("5c317d28-3d67-4979-84e2-993be760edeb"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "character varying(1500)",
                maxLength: 1500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1500)",
                oldMaxLength: 1500,
                oldNullable: true);
        }
    }
}
