using ClusterVR.CreatorKit.Editor.Utils;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class EditAndUploadVenueView : VisualElement
    {
        readonly EditVenueView editVenueView;
        readonly UploadVenueView uploadVenueView;

        public EditAndUploadVenueView()
        {
            editVenueView = new EditVenueView();
            hierarchy.Add(editVenueView);

            uploadVenueView = new UploadVenueView();
            hierarchy.Add(uploadVenueView);
        }

        public Disposable Bind(EditAndUploadVenueViewModel viewModel)
        {
            return Disposable.Create(
                editVenueView.Bind(viewModel.EditVenueViewModel),
                uploadVenueView.Bind(viewModel.UploadVenueViewModel));
        }
    }
}
