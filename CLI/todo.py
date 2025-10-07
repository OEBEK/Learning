import sys
import argparse
from datetime import datetime
import json
import os 


class Person:
    def __init__(self, name, alter):
        self.name = name   # Attribut
        self.alter = alter # Attribut
    def hallo(self):       # Methode
        print(f"Hallo, ich heiße {self.name} und bin {self.alter} Jahre alt.")

class Task:
    def __init__(self, titel, description="", status="offen", created_at=None):
        self.titel = titel   # Attribut
        self.description = description # Attribut
        # self.due_date = due_date # Attribut
        self.status = status # Attribut
        self.created_at = created_at if created_at else datetime.now()


    def info(self):   # Methode
         print(
            f"Task: {self.titel}\n"
            f"Beschreibung: {self.description}\n"
            f"Status: {self.status}\n"
            f"Erstellt am: {self.created_at.strftime('%Y-%m-%d %H:%M:%S')}"
        )
    def to_dict(self):
        return {"Titel": self.titel, "Beschreibung": self.description,"Status": self.status, "Erstellt": self.created_at.isoformat()}

def main():
    datei = "todos.json"

    # Wenn Datei noch nicht existiert, leere Liste anlegen
    if os.path.exists(datei):
        with open(datei, "r", encoding="utf-8") as f:
            AufgabenListe = json.load(f)
    else:
        AufgabenListe = []
    

    # CLI Setup
    parser = argparse.ArgumentParser(prog="todo.py")
    subparsers = parser.add_subparsers(dest="command", required=True)
    
    # list command
    subparsers.add_parser("list", help="Zeigt alle Aufgaben an")

    # delete command 
    delete_parser = subparsers.add_parser("delete", help="Lösche eine Aufgabe")
    delete_parser.add_argument("nummer", type=int, help="Nummer der Aufgabe, die gelöscht werden soll") 
    
    done_parser = subparsers.add_parser("done", help="Lösche eine Aufgabe")
    done_parser.add_argument("nummerDone", type=int, help="Nummer der Aufgabe, die fertig ist") 


    add_parser = subparsers.add_parser("add", help="Füge eine neue Aufgabe hinzu")
    add_parser.add_argument("titel", help="titel")
    add_parser.add_argument("-d","--description", help="Die Beschreibung", default="")             # Pflichtargument
   
    args = parser.parse_args()

    # --- Command Handling ---
    if args.command == "add":
        task = Task(args.titel, args.description)
        AufgabenListe.append(task.to_dict())
        task.info()
        # Datei mit aktualisierter Liste speichern
        with open(datei, "w", encoding="utf-8") as f:
            json.dump(AufgabenListe, f, indent=4, ensure_ascii=False)
        print("Aufgabe gespeichert!")
    
    elif args.command == "list":
    # JSON-Datei laden
        with open(datei, "r", encoding="utf-8") as f:
            daten = json.load(f)

        if not daten:
            print("Keine Aufgaben vorhanden!")
        else:
            print("\n=== Aufgabenliste ===\n")
            for i, aufgabe in enumerate(daten, start=1):
                print(f"Aufgabe {i}")
                print(f"  Titel       : {aufgabe.get('Titel', '-')}")
                print(f"  Beschreibung: {aufgabe.get('Beschreibung','-')}")
                print(f"  Status      : {aufgabe.get('Status','-')}")
                print(f"  Erstellt am : {aufgabe.get('Erstellt','-')}")
                print("-" * 40)

    elif args.command == "delete":
        with open(datei, "r", encoding="utf-8") as f:
            daten = json.load(f)
        index = args.nummer - 1  
        # Example: delete the first task (index 0)
        del daten[index]

        with open(datei, "w", encoding="utf-8") as f:
            json.dump(daten, f, indent=4, ensure_ascii=False)

        print("Deleted successfully!")
    
    elif args.command == "done":
        with open(datei, "r", encoding="utf-8") as f:
            daten = json.load(f)
        index = args.nummer - 1  
        # Example: delete the first task (index 0)
        print(daten[index])

        with open(datei, "w", encoding="utf-8") as f:
            json.dump(daten, f, indent=4, ensure_ascii=False)

        print("Deleted successfully!")

if __name__ == "__main__":
    main()

 