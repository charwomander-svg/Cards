using System;
using Cards.Xbox;

class UiStartTest
{
    static int Main()
    {
        var vm = new CollectionUiViewModel();
        // ensure filter cleared
        vm.ApplyFilter(string.Empty);
        // pick 4th visible game (1-based)
        int idx = 3;
        vm.MoveSelection(idx - vm.SelectedGameIndex);
        Console.WriteLine($"Selected: {vm.SelectedGame.Name}");
        var result = vm.StartSelectedGame();
        Console.WriteLine($"Start result: {result.Succeeded} - {result.Message}");
        return 0;
    }
}
