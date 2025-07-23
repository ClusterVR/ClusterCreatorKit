using ClusterVR.CreatorKit.Editor.AccessoryExporter;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter;
using ClusterVR.CreatorKit.ItemExporter;
using ClusterVR.CreatorKit.Translation;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{

    public interface IItemBuilderDependencies
    {
        bool IsBetaAllowedItemType { get; }
        string EditorTypeName { get; }
        string GetUploadedItemsManagementUrl();
        UploadingItemRepository ItemRepository { get; }
        IItemExporter ItemExporter { get; }
        IComponentValidator ComponentValidator { get; }
        IGltfValidator GltfValidator { get; }
        IItemTemplateBuilder ItemTemplateBuilder { get; }
        IItemUploadService UploadService { get; }
    }

    public abstract class ItemBuilderDependencies<TItemExporter, TComponentValidator, TGltfValidator, TItemTemplateBuilder, TItemUploadService>
    : IItemBuilderDependencies
        where TItemExporter : IItemExporter, new()
        where TComponentValidator : IComponentValidator, new()
        where TGltfValidator : IGltfValidator, new()
        where TItemTemplateBuilder : IItemTemplateBuilder, new()
        where TItemUploadService : IItemUploadService, new()
    {
        public abstract string EditorTypeName { get; }
        public abstract bool IsBetaAllowedItemType { get; }
        public abstract string GetUploadedItemsManagementUrl();
        public abstract UploadingItemRepository ItemRepository { get; }
        IItemExporter IItemBuilderDependencies.ItemExporter { get; } = new TItemExporter();
        IComponentValidator IItemBuilderDependencies.ComponentValidator { get; } = new TComponentValidator();
        IGltfValidator IItemBuilderDependencies.GltfValidator { get; } = new TGltfValidator();
        IItemTemplateBuilder IItemBuilderDependencies.ItemTemplateBuilder { get; } = new TItemTemplateBuilder();
        IItemUploadService IItemBuilderDependencies.UploadService { get; } = new TItemUploadService();
    }

    public sealed class CraftItemBuilderDependencies : ItemBuilderDependencies<
        CraftItemExporter,
        CraftItemComponentValidator,
        CraftItemValidator,
        CraftItemTemplateBuilder,
        UploadCraftItemTemplateService
    >
    {
        public override bool IsBetaAllowedItemType => true;
        public override string EditorTypeName => TranslationTable.cck_common_item;
        public override string GetUploadedItemsManagementUrl() => Constants.WebBaseUrl + "/account/contents/items";
        public override UploadingItemRepository ItemRepository => UploadingItemRepository.CraftItems;
    }

    public sealed class AccessoryItemBuilderDependencies : ItemBuilderDependencies<
        CreatorKit.AccessoryExporter.AccessoryExporter,
        AccessoryComponentValidator,
        AccessoryValidator,
        AccessoryTemplateBuilder,
        UploadAccessoryTemplateService
    >
    {
        public override bool IsBetaAllowedItemType => false;
        public override string EditorTypeName => TranslationTable.cck_accessory;
        public override string GetUploadedItemsManagementUrl() => Constants.WebBaseUrl + "/account/contents/accessories";
        public override UploadingItemRepository ItemRepository => UploadingItemRepository.Accessories;
    }
}
