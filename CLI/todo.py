import sys
import argparse
from datetime import datetime
import json
import os 
import storage
from storage import Task
from tasks import add_task, list_task, delete_task



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
        add_task(args.titel, args.description, AufgabenListe)
     
    
    elif args.command == "list":
        list_task()
       

    elif args.command == "delete":
        delete_task(args.nummer)
     
    
    elif args.command == "done":
        with open(datei, "r", encoding="utf-8") as f:
            daten = json.load(f)
        index = args.nummerDone - 1  
        # Example: delete the first task (index 0)
        
        item = daten[index]
        item["Status"] = "done"

        with open(datei, "w", encoding="utf-8") as f:
            json.dump(daten, f, indent=4, ensure_ascii=False)

        print("Task set Finished")

if __name__ == "__main__":
    main()

 