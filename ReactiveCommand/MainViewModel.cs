using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using ReactiveUI;

namespace ReactiveCommandTest
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel()
        {
            PickFirstCommand = ReactiveCommand.CreateFromObservable(() => Observable.FromAsync(PickFile).Where(x => x!=null));
            PickSecondCommand = ReactiveCommand.CreateFromObservable(() => Observable.FromAsync(PickFile).Where(x => x != null));
            MergeCommand = ReactiveCommand.CreateFromObservable(() => PickFirstCommand.CombineLatest(PickSecondCommand, (a, b) => Unit.Default));
        }

        public ReactiveCommand<Unit, Unit> MergeCommand { get; }

        public ReactiveCommand<Unit, StorageFile> PickSecondCommand { get; }

        public ReactiveCommand<Unit, StorageFile> PickFirstCommand { get; }

        private async Task<StorageFile> PickFile()
        {
            var picker = new FileOpenPicker()
            {
                CommitButtonText = "Pick",                
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };


            picker.FileTypeFilter.Add("*");
            return await picker.PickSingleFileAsync();
        }
    }
}