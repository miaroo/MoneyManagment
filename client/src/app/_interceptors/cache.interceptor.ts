import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { share } from 'rxjs/operators';

@Injectable()
export class Cache implements HttpInterceptor {

  private cache: Map<HttpRequest<any>, HttpResponse<any>> = new Map();

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(request.method !== "GET") {
      return next.handle(request)
    }
    const cachedResponse: HttpResponse<any> = this.cache.get(request)
    if(cachedResponse) 
    {
      return of(cachedResponse.clone())
    }
    else
    {
      return next.handle(request).pipe()
    }
  }
}
