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
        private IRegistersDataService _registersDataService { get; set; }

        [Inject]
        private IRegisterEntriesDataService _registerEntriesDataService { get; set; }

        [Inject]
        private IBudgetCyclesDataService _budgetCyclesDataService { get; set; }
        [Inject]
        private IBudgetItemsDataService _budgetItemsDataService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;
        private int SelectedId { get; set; }

        private bool HideReconciled { get; set; }  = false;
        private bool HideRegisterName { get; set; } = true;

        private RegisterEntry NewEntry { get; set; }

        private Stack<UndoArgs> UndoStack { get; set; }
        private Stack<UndoArgs> RedoStack { get; set; }

        public RegisterBook()
        {
            UndoStack = new Stack<UndoArgs>();
            RedoStack = new Stack<UndoArgs>();
        }


        protected override async Task OnInitializedAsync()
        {
            var result2 = await _budgetCyclesDataService.Get(includeRelated: false);
            BudgetCycles = result2.ToList();
            var CurrentBudgetCycle = GetBudgetCycleByDate(DateTime.Today);

            var result3 = await _budgetItemsDataService.Get(includeRelated: false);
            BudgetItems = result3.ToList();

            NewEntry = new RegisterEntry()
            {
                RegisterId = Register.Id,
                BudgetCycleId = CurrentBudgetCycle != null ? CurrentBudgetCycle.Id : 0,
                BudgetItemId = 0
            };

        }

        private BudgetCycle GetBudgetCycleByDate(DateTime date)
        {
            if (BudgetCycles == null)
                return null;

            foreach(var bc in BudgetCycles)
            {
                if (date >= bc.StartDate && date <= bc.EndDate)
                    return bc;
            }

            return null;
        }

        private async Task AddEntry(RegisterEntry entry)
        {
            bool success = await _registerEntriesDataService.Create(entry);
            if (success)
            {
                _toastService.ShowSuccess("Add Entry Successful", "");
                var result = await _registerEntriesDataService.Get(includeRelated: true);
                Register.RegisterEntries = result.ToList();
                Register.Balance();
            }
        }

        private async Task<bool> RefreshEntries()
        {
            var result = await _registerEntriesDataService.Get(includeRelated: true);
            if (result == null)
                return false;
            Register.RegisterEntries = result.ToList();
            Register.Balance();
            return true;
        }

        private async Task<bool> UpdateEntry(int id, RegisterEntry entry)
        {
            bool success = await _registerEntriesDataService.Update(id, entry);
            if (success)
            {
                var result = await _registerEntriesDataService.Get(includeRelated: true);
                Register.RegisterEntries = result.ToList();
                Register.Balance();
            }
            return success;
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
            RegisterEntry entry = await _registerEntriesDataService.Get(id, includeRelated: false);
            if (entry == null)
                return;

            // save the status
            var status = entry.Status;

            // change status and save
            entry.Status = (int)entry.NextStatus();
            bool success = await UpdateEntry(id, entry);
            if (success)
            {
                entry.Status = status;
                UndoStack.Push(new UndoArgs() { Action = RegisterEntryAction.Modify, Entry = entry });
            }

        }

        private async Task IncrementDate(int id)
        {
            RegisterEntry entry = await _registerEntriesDataService.Get(id, includeRelated: false);
            if (entry == null)
                return;

            // save the date
            var date = entry.TransactionDate;

            // change date and save
            entry.TransactionDate = entry.TransactionDate.AddDays(1);
            bool success = await UpdateEntry(id, entry);

            if (success)
            {
                entry.TransactionDate = date;
                UndoStack.Push(new UndoArgs() { Action = RegisterEntryAction.Modify, Entry = entry });
            }
        }

        private async Task DecrementDate(int id)
        {
            RegisterEntry entry = await _registerEntriesDataService.Get(id, includeRelated: false);
            if (entry == null)
                return;

            // save the date
            var date = entry.TransactionDate;

            // change date and save
            entry.TransactionDate = entry.TransactionDate.AddDays(-1);
            bool success = await UpdateEntry(id, entry);

            if (success)
            {
                entry.TransactionDate = date;
                UndoStack.Push(new UndoArgs() { Action = RegisterEntryAction.Modify, Entry = entry });
            }

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
                bool success = await _registerEntriesDataService.Delete(SelectedId);
                if (success)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    var result = await _registerEntriesDataService.Get(includeRelated: true);
                    Register.RegisterEntries = result.ToList();
                }
                else
                {
                    _toastService.ShowWarning("Delete Failed", "");
                }
            }

        }

        public async void Reconcile()
        {
            var success = await _registersDataService.Reconcile(Register.Id);
            if (success)
                await RefreshEntries();
        }

        public void ToggleHideReconciled()
        {
            HideReconciled = !HideReconciled;
        }


        // Make UndoRedo a Service
        public async Task Undo()
        {
            if (UndoStack.Count == 0)
                return;
            var undoArgs = UndoStack.Pop();
            //RedoStack.Push(undoArgs);
            if(undoArgs.Action == RegisterEntryAction.Modify)
            {
                await UpdateEntry(undoArgs.Entry.Id, undoArgs.Entry);
            }
        }

        public async Task Redo()
        {
            if (RedoStack.Count == 0)
                return;
            var undoArgs = RedoStack.Pop();
            UndoStack.Push(undoArgs);
            if (undoArgs.Action == RegisterEntryAction.Modify)
            {
                await UpdateEntry(undoArgs.Entry.Id, undoArgs.Entry);
            }
        }
    }
}
