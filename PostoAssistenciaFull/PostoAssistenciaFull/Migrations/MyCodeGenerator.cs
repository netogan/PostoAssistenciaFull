using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace PostoDeAssistenciaWeb.Migrations
{
    internal class MyCodeGenerator : CSharpMigrationCodeGenerator
    {
        private string TrimSchemaPrefix(string table)
        {
            if (table.StartsWith("dbo."))
                return table.Replace("dbo.", "");

            return table;
        }

        protected override void Generate(AddColumnOperation addColumnOperation, IndentedTextWriter writer)
        {
            var add = new AddColumnOperation(TrimSchemaPrefix(addColumnOperation.Table), addColumnOperation.Column);
            base.Generate(add, writer);
        }

        protected override void Generate(AddForeignKeyOperation addForeignKeyOperation, IndentedTextWriter writer)
        {
            addForeignKeyOperation.PrincipalTable = TrimSchemaPrefix(addForeignKeyOperation.PrincipalTable);
            addForeignKeyOperation.DependentTable = TrimSchemaPrefix(addForeignKeyOperation.DependentTable);
            base.Generate(addForeignKeyOperation, writer);
        }

        protected override void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation, IndentedTextWriter writer)
        {
            addPrimaryKeyOperation.Table = TrimSchemaPrefix(addPrimaryKeyOperation.Table);
            base.Generate(addPrimaryKeyOperation, writer);
        }

        protected override void Generate(AlterColumnOperation alterColumnOperation, IndentedTextWriter writer)
        {
            AlterColumnOperation alter = null;
            if (alterColumnOperation.Inverse != null)
                alter = new AlterColumnOperation(TrimSchemaPrefix(alterColumnOperation.Table), alterColumnOperation.Column, alterColumnOperation.IsDestructiveChange, (AlterColumnOperation)alterColumnOperation.Inverse);
            else
                alter = new AlterColumnOperation(TrimSchemaPrefix(alterColumnOperation.Table), alterColumnOperation.Column, alterColumnOperation.IsDestructiveChange);

            if (alter != null)
                base.Generate(alter, writer);
            else
                base.Generate(alterColumnOperation);
        }

        protected override void Generate(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
        {
            createIndexOperation.Table = TrimSchemaPrefix(createIndexOperation.Table);
            base.Generate(createIndexOperation, writer);
        }

        protected override void Generate(CreateTableOperation createTableOperation, IndentedTextWriter writer)
        {
            var create = new CreateTableOperation(TrimSchemaPrefix(createTableOperation.Name));

            foreach (var item in createTableOperation.Columns)
                create.Columns.Add(item);

            create.PrimaryKey = createTableOperation.PrimaryKey;

            base.Generate(create, writer);
        }

        protected override void Generate(DropColumnOperation dropColumnOperation, IndentedTextWriter writer)
        {
            var drop = new DropColumnOperation(TrimSchemaPrefix(dropColumnOperation.Table), dropColumnOperation.Name);
            base.Generate(drop, writer);
        }

        protected override void Generate(DropForeignKeyOperation dropForeignKeyOperation, IndentedTextWriter writer)
        {
            dropForeignKeyOperation.PrincipalTable = TrimSchemaPrefix(dropForeignKeyOperation.PrincipalTable);
            dropForeignKeyOperation.DependentTable = TrimSchemaPrefix(dropForeignKeyOperation.DependentTable);
            base.Generate(dropForeignKeyOperation, writer);
        }

        protected override void Generate(DropIndexOperation dropIndexOperation, IndentedTextWriter writer)
        {
            dropIndexOperation.Table = TrimSchemaPrefix(dropIndexOperation.Table);
            base.Generate(dropIndexOperation, writer);
        }

        protected override void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation, IndentedTextWriter writer)
        {
            dropPrimaryKeyOperation.Table = TrimSchemaPrefix(dropPrimaryKeyOperation.Table);
            base.Generate(dropPrimaryKeyOperation, writer);
        }

        protected override void Generate(DropTableOperation dropTableOperation, IndentedTextWriter writer)
        {
            var drop = new DropTableOperation(TrimSchemaPrefix(dropTableOperation.Name));
            base.Generate(drop, writer);
        }

        protected override void Generate(MoveTableOperation moveTableOperation, IndentedTextWriter writer)
        {
            var move = new MoveTableOperation(TrimSchemaPrefix(moveTableOperation.Name), moveTableOperation.NewSchema);
            base.Generate(move, writer);
        }

        protected override void Generate(RenameColumnOperation renameColumnOperation, IndentedTextWriter writer)
        {
            var rename = new RenameColumnOperation(TrimSchemaPrefix(renameColumnOperation.Table), renameColumnOperation.Name, renameColumnOperation.NewName);
            base.Generate(rename, writer);
        }

        protected override void Generate(RenameTableOperation renameTableOperation, IndentedTextWriter writer)
        {
            var rename = new RenameTableOperation(TrimSchemaPrefix(renameTableOperation.Name), renameTableOperation.NewName);
            base.Generate(rename, writer);
        }
    }

}