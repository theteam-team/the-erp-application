import { Component, OnInit } from '@angular/core';
import { MaterialService } from '../services/material.service';
import { BillofMatrials } from '../modules/module';

@Component({
  selector: 'app-bill-of-materials',
  templateUrl: './bill-of-materials.component.html',
  styles: []
})
export class BillOfMaterialsComponent implements OnInit {

    materials: BillofMatrials [];
    constructor(private materialService: MaterialService) { }

    ngOnInit() {
        this.materialService.getMaterials().subscribe(materials => this.materials = materials);
  }

}
