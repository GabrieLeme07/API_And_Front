import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ImportService } from 'src/app/services/import.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
 
  public idConsulta: any = '';
  public tableI: any;
  elements: any = [];
  headElements = ['id', 'Data Importacao', 'Numero Registros', 'NumeroTotal Produtos', 'Valor Total Importado', 'menor Data Entrega '];
  importacoes: any[];
  previous: string;
  
  constructor(
    private importacaoService: ImportService,
    private router: Router
  ) { }
  
  
  ngOnInit(): void {
    this.getImportacoes();
    // this.tableI.setDataSource(this.importacoes);
  }

  getImportacoes() {
    this.importacaoService.getAll().subscribe((importacao: any[]) => {
      this.importacoes = importacao;
      // this.tableI.setDataSource(this.importacoes);
      console.log(importacao);
    })
  }

  onClickGetById() {
    const guid = this.idConsulta
    if (this.isGuid(guid)) {      
        this.router.navigate(['/tablesbyid'],
          { state: {guid: guid}});          
    } else {
      alert("informe um ID")
    }
  }

  isGuid(guid: string): boolean{
    var IsGuid: boolean = false;
    if (guid != null) {
     var guidRegExp: RegExp = new RegExp("^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
      IsGuid = guidRegExp.test(guid)
    }
    return IsGuid;
  }
}
