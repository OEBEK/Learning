from datetime import datetime
import json

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

datei = "todos.json"


def load_tasks():
    with open(datei, "r", encoding="utf-8") as f:
        return json.load(f)

def save_tasks(tasks):
    with open(datei, "w", encoding="utf-8") as f:
        json.dump(tasks, f, indent=4)