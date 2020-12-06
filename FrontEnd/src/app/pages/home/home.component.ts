import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Arquivo } from 'src/app/models/arquivo';
import { LoteImportacao } from 'src/app/models/lote-importacao';
import { ImportService } from 'src/app/services/import.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  file = {} as Arquivo;
  webFile: any = null;
  lotes: LoteImportacao[];

  constructor(
    private importacaoService: ImportService,
    private router: Router,
  ){}

  ngOnInit(): void {
  }
  
  onFileChange(event : any) {    
      this.webFile = event.target.files[0];   
  }
  
  uploadFile(){
    if(!this.webFile){
      alert("")
    }
    else{      
      let reader : FileReader = new FileReader();

      reader.onloadend = (e) => {
        let base64 = reader.result.toString();
        this.file.nome = this.webFile.name;
        this.file.base64 = base64.split(",")[1];
        this.importacaoService
          .postImportacao(this.file)
          .subscribe((result: any) => {
                this.router.navigate(['/table']);
          }, error => { error }
          );
      }
      reader.readAsDataURL(this.webFile);
    }
  }

}
