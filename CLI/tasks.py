from storage import Task, save_tasks
import sys
import argparse
from datetime import datetime
import json
import os 


datei = "todos.json"



def add_task(titel, desc, list):
    task = Task(titel, desc)
    list.append(task.to_dict())
    task.info()
    # Datei mit aktualisierter Liste speichern
    save_tasks(list)
    
    print("Aufgabe gespeichert!")


def list_task():
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

def delete_task(number):
    with open(datei, "r", encoding="utf-8") as f:
        daten = json.load(f)
    index = number - 1  
    # Example: delete the first task (index 0)
    del daten[index]
        
    with open(datei, "w", encoding="utf-8") as f:
        json.dump(daten, f, indent=4, ensure_ascii=False)

    print("Deleted successfully!")