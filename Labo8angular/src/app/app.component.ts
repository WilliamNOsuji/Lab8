import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Animal } from 'src/models/animal';
import {lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  // Inputs
  id ?: number;
  type : string = "";
  name : string = "";

  animals : Animal[] = [];
  animal ?: Animal;

  constructor(public http : HttpClient){}

  // Récupère tous les animaux dans la base de données
  async getAnimals() : Promise<void>{
	// A
    let x = await lastValueFrom(this.http.get<Animal[]>("https://localhost:7192/api/Animals/GetAnimal"))
    console.log(x)
    //this.animals = x;
  }

  // Ajoute un animal dans la base de données
  async postAnimal() : Promise<void>{
    // N
    let newAnimal = new Animal(0, this.type, this.name);

    let x = await lastValueFrom(this.http.post<Animal>("https://localhost:7192/api/Animals/PostAnimal", newAnimal))
    console.log(x)
  }

  // Récupère un animal en particulier dans la base de données
  async getAnimal() : Promise<void>{
    // I
    if(this.id != undefined){
      let x = await lastValueFrom(this.http.get<Animal>("https://localhost:7192/api/Animals/GetAnimal/"+this.id))
      console.log(x)
    }
  }

  // Modifie (ou crée) un animal en particulier dans la base de données
  async putAnimal() : Promise<void>{
    // M
    if(this.id != undefined){
      let updatedAnimal = new Animal(this.id, this.type, this.name)

      let x = await lastValueFrom(this.http.put<Animal>("https://localhost:7192/api/Animals/PutAnimal/"+this.id, updatedAnimal));
      console.log(x)
    }
  }

  // Supprime un animal en particulier dans la base de données
  async deleteAnimal() : Promise<void>{
    // A
    if(this.id != undefined){
      let x = await lastValueFrom(this.http.post<Animal>("https://localhost:7192/api/destroy/"+this.id,null));
      console.log(x)
    }
  }

  // Sussy function
  async deleteAll() : Promise<void>{
    // L
    let x = await lastValueFrom(this.http.post("https://localhost:7192/api/Animals/clear-all", null));
    console.log("All animals deleted", x);
  }

}
