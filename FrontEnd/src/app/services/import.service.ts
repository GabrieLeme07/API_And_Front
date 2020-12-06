import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoteImportacao } from '../models/lote-importacao';
import { Importacao } from '../models/importacao';
import { Arquivo } from '../models/arquivo';

@Injectable({
  providedIn: 'root'
})
export class ImportService {

  constructor(private httpClient: HttpClient) { }

  baseUrl = 'http://localhost:64645/api/Importacao';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  getAll(): Observable<LoteImportacao[]> {
    return this.httpClient
      .get<LoteImportacao[]>(this.baseUrl + '/' +'getall')
      .pipe()
  }

  getById(id: string): Observable<Importacao[]> {
    return this.httpClient
      .get<Importacao[]>(this.baseUrl + '/getbyid/' + id)
      .pipe()
  }

  postImportacao(arquivo: Arquivo): Observable<any> {
    return this.httpClient
      .post((this.baseUrl + "/" + "uploadexcel"), arquivo)
      .pipe();
  }
}
