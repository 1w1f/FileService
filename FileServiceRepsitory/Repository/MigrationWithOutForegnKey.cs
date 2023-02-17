using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace FileServiceRepsitory.Repository;

public class MigrationWithOutForegnKey : MigrationsModelDiffer
{
    public MigrationWithOutForegnKey(
        IRelationalTypeMappingSource typeMappingSource,
        IMigrationsAnnotationProvider migrationsAnnotations,
        IChangeDetector changeDetector,
        IUpdateAdapterFactory updateAdapterFactory,
        CommandBatchPreparerDependencies commandBatchPreparerDependencies
    )
        : base(
            typeMappingSource,
            migrationsAnnotations,
            changeDetector,
            updateAdapterFactory,
            commandBatchPreparerDependencies
        )
    { }

    public override IReadOnlyList<MigrationOperation> GetDifferences(
        IRelationalModel source,
        IRelationalModel target
    )
    {
        // return base.GetDifferences(source, target);
        var operations = base.GetDifferences(source, target)
            .Where(op => op is not AddForeignKeyOperation)
            .Where(op => op is not DropForeignKeyOperation)
            .ToList();
        foreach (var operation in operations.OfType<CreateTableOperation>())
            operation.ForeignKeys?.Clear();
        return operations;
    }
}
