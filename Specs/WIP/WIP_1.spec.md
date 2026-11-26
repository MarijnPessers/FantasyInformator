# Spinner, error messages and snackbar

1. A spinner as the loading image, in `FantasyInformator.App/wwwroot/`, shown while the database wakes.
2. A state that holds the error messages and catches the exceptions, through `IProgress<ProgressReport>`.
3. A snackbar that shows the error messages.
4. Decide where `FocusOwnerName` lives: `appsettings.json` or user secrets. The App README says personal values go in user secrets.
5. Test the success path of `DatabaseWakeUpService`. EF's InMemory provider can't run `SELECT 1`.
