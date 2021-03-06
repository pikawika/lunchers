import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MerchantDataService } from '../merchant-data.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Ingredient } from 'src/models/Ingredient';
import { Tag } from 'src/models/Tag';

@Component({
  selector: 'app-add-lunch',
  templateUrl: './add-lunch.component.html',
  styleUrls: ['./add-lunch.component.css']
})
export class AddLunchComponent implements OnInit {

  public lunch: FormGroup;
  public errorMsg: string;
  filesToUpload: Array<File> = [];
  public _ingredienten = [];
  public _tags = [];
  public _selectedTags = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private merchantService: MerchantDataService
  ) { }


  ngOnInit() {

    this.merchantService.getTag().subscribe(data => {
      this._tags = data;


      let instructionTag = new Tag();
      instructionTag.Naam = "Gelieve een tag te kiezen uit de lijst";
      instructionTag.tagId = -2;
      this._selectedTags.push(instructionTag);

    });


    this.lunch = this.fb.group({
      name: [
        '',
        [Validators.required, Validators.minLength(4)]
      ],
      price: [
        '',
        [Validators.required]
      ],
      description: [
        '',
        [Validators.required, Validators.minLength(10)]
      ],
      startdate: [
        ''
      ],
      enddate: [
        ''
      ],
      ingredient: [
        ''
      ]
    });
  }

  selectTag(tag: Tag) {
    if (this._selectedTags.some(e => e.tagId == -2)) {
      this._selectedTags = [];
    }

    if (!this._selectedTags.some(e => e.Naam === tag.Naam)) {
      this._selectedTags.push(tag);
    }
  }

  removeSelectedTag(tag) {
    for (let i = 0; i < this._selectedTags.length; i++) {
      if (this._selectedTags[i].Naam == tag.Naam) {
        this._selectedTags.splice(i, 1);
        if (this._selectedTags.length == 0) {
          let instructionTag = new Tag();
          instructionTag.Naam = "Gelieve een tag te kiezen uit de lijst";
          instructionTag.tagId = -2;
          this._selectedTags.push(instructionTag);
        }
        break;
      }
    }
  }

  addIngredient(ingredient: string) {
    let ing = new Ingredient();
    if (ingredient.length <= 20 && ingredient.length > 0) {
      ing.Naam = ingredient;
      this._ingredienten.push(ing);
      (<HTMLInputElement>document.getElementById('ingredienten')).value = "";
    } else {
      this.errorMsg = "Er is een fout opgetreden bij het toevoegen van een ingredient(max 20 tekens)"
    }
  }



  removeIng(ingredient) {
    for (let i = 0; i < this._ingredienten.length; i++) {
      if (this._ingredienten[i].Naam == ingredient.Naam) {
        this._ingredienten.splice(i, 1);
        break;
      }
    }
  }





  onSubmit() {
    const data: any = new FormData();
    const files: File[] = this.filesToUpload;

    if (files.length > 1) {
      for (var x = 0; x < files.length; x++) {
        data.append('afbeeldingen', files[x]);
      }
    } else {
      data.append('afbeeldingen', files[0]);
    }
    data.append("naam", this.lunch.value.name);
    data.append("prijs", Number(this.lunch.value.price));
    data.append("beschrijving", this.lunch.value.description);
    data.append("beginDatum", (<HTMLInputElement>document.getElementById('startdate')).value);
    data.append("eindDatum", (<HTMLInputElement>document.getElementById('enddate')).value);
    data.append("ingredienten", JSON.stringify(this._ingredienten));
    data.append("tags", JSON.stringify(this._selectedTags));

    this.merchantService.addLunch(
      data)
      .subscribe(
        val => {
          if (val) {
            this.router.navigate(['/merchant/lunch']);
          }
        },
        (error: HttpErrorResponse) => {
          this.errorMsg = error.error.error;
        }
      );

  }


  fileChange(event) {
    this.filesToUpload = event.target.files;
  }

  ngAfterViewInit() {
    var startdate = (<HTMLInputElement>document.getElementById('startdate'));
    var enddate = (<HTMLInputElement>document.getElementById('enddate'));
    startdate.value = new Date().toISOString().slice(0, 10);
    enddate.value = new Date().toISOString().slice(0, 10);
  }
}


