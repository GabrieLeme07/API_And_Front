import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Importacao } from 'src/app/models/importacao';
import { ImportService } from 'src/app/services/import.service';

@Component({
  selector: 'app-tablesgetby',
  templateUrl: './tablesgetby.component.html',
  styleUrls: ['./tablesgetby.component.css']
})
export class TablesgetbyComponent implements OnInit {

  Guid: string;
  data: any[];
  headElements = ['Data Entrega ', 'Descrição do Produto ', 'Valor Unitario: ', 'Quantidade', 'Valor Total'];
  
  constructor(
    private importacaoService: ImportService,
    private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.Guid = nav.extras.state.guid;
    } 

  ngOnInit(): void {
    this.getById(this.Guid)
  }

  getById(guid: string) {
    this.importacaoService.getById(guid).subscribe((data: any[]) => {
      this.data = data;
      // this.tableI.setDataSource(this.importacoes);
      console.log(data);
    })
  }
}
