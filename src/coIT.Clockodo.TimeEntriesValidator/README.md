# coIT.Clockodo.TimeEntriesValidator

WinForms Applikation mit der man über einen Bestimmten Zeitraum von ausgewählten Mitarbeitern Fehlerhafte Zeiteinträge rausfilern kann

![Bild der TimeEntriesValidator Anwendung](assets/images/TimeEntriesValidator.PNG)

### Programmaufbau

- BusinessRules 
Hier werden Regeln festgelegt, welche bei der Prüfung von den Zeiteinträgen erfüllt werden muss. 
Bei Fehlern wird eine String mit der Fehlerbeschreibung zurückgegeben



### Konfiguration
 
Die Konfiguration liegt in der Settings Klasse.
Über die Methode Credentials werden die Zugangsdaten für die API ausgelesen


### Filter

Man kann über den Start und Endzeitpunkt Filtern
Man kann über jeden einzelnen Mitarbeiter Filtern 

### Extern benutzte Projekte/Libraries

Benutzt folgende Libraries: 
* coIT.Libraries.Clockodo
