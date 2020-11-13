using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Components
{
    public partial class RegisterBook
    {
        [Parameter]
        public Register Register { get; set; }

        [Parameter]
        public IList<BudgetCycle> BudgetCycles { get; set; }

        [Parameter]
        public IList<BudgetItem> BudgetItems { get; set; }

        [Inject]
        private IRegisterEntriesDataService _dataService { get; set; }

        [Inject]
        private IBudgetCyclesDataService _budgetCyclesDataService { get; set; }
        [Inject]
        private IBudgetItemsDataService _budgetItemsDataService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;
        private int SelectedId { get; set; }

        private bool HideReconciled = false;

        private RegisterEntry NewEntry { get; set; }

        private Stack<UndoArgs> UndoStack { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var result2 = await _budgetCyclesDataService.Get(includeRelated: false);
            BudgetCycles = result2.ToList();

            var result3 = await _budgetItemsDataService.Get(includeRelated: false);
            BudgetItems = result3.ToList();

            NewEntry = new RegisterEntry()
            {
                RegisterId = Register.Id,
                SelectedRegisterId = Register.Id.ToString()
            };

        }

        private async Task AddEntry(RegisterEntry entry)
        {
            bool success = await _dataService.Create(entry);
            if (success)
            {
                _toastService.ShowSuccess("Add Entry Successful", "");
                var result = await _dataService.Get(includeRelated: true);
                Register.RegisterEntries = result.ToList();
                Register.Balance();
            }
        }

        private async Task UpdateEntry(int id, RegisterEntry entry)
        {
            bool success = await _dataService.Update(id, entry);
            if (success)
            {
                var result = await _dataService.Get(includeRelated: true);
                Register.RegisterEntries = result.ToList();
                Register.Balance();
            }
        }

        public async Task OnAction(RegisterEntryActionEventArgs args)
        {
            switch (args.Action)
            {
                case RegisterEntryAction.NextStatus:
                    await NextStatus(args.Id);
                    break;

                case RegisterEntryAction.IncrementDate:
                    await IncrementDate(args.Id);
                    break;

                case RegisterEntryAction.DecrementDate:
                    await DecrementDate(args.Id);
                    break;

                case RegisterEntryAction.Delete:
                    await Delete(args.Id);
                    break;

                default:
                    break;
            }
        }

        private async Task NextStatus(int id)
        {
            RegisterEntry entry = await _dataService.Get(id, includeRelated: false);
            UndoStack.Push(new UndoArgs() { Action = RegisterEntryAction.Modify, Entry = entry });
            entry.Status = (int)entry.NextStatus();

            await UpdateEntry(id, entry);

        }

        private async Task IncrementDate(int id)
        {
            RegisterEntry entry = await _dataService.Get(id, includeRelated: false);
            UndoStack.Push(new UndoArgs() { Action = RegisterEntryAction.Modify, Entry = entry });
            entry.TransactionDate = entry.TransactionDate.AddDays(1);

            await UpdateEntry(id, entry);

        }

        private async Task DecrementDate(int id)
        {
            RegisterEntry entry = await _dataService.Get(id, includeRelated: false);
            UndoStack.Push(new UndoArgs() { Action = RegisterEntryAction.Modify, Entry = entry });
            entry.TransactionDate = entry.TransactionDate.AddDays(-1);

            await UpdateEntry(id, entry);

        }

        private Task Delete(int id)
        {
            SelectedId = id;
            ShowDeleteDialog = true;

            return Task.CompletedTask;
        }

        private async Task OnDeleteClose(bool accepted)
        {
            ShowDeleteDialog = false;
            if (accepted)
            {
                bool success = await _dataService.Delete(SelectedId);
                if (success)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    var result = await _dataService.Get(includeRelated: true);
                    Register.RegisterEntries = result.ToList();
                }
                else
                {
                    _toastService.ShowWarning("Delete Failed", "");
                }
            }

        }



    }
}
