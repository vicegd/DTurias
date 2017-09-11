﻿import { Injectable } from "@angular/core";
import { Http, RequestMethod, Request, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import { Filter, Pagination } from "./configClasses.repository";
import { Tweet } from "./tweet.model";

const tweetsUrl = "http://localhost:54781/api/tweets";

@Injectable()
export class Repository {
    tweets: Tweet[];
    count: number;
    private filterObject = new Filter();
    private paginationObject = new Pagination();

    constructor(private http: Http) {
        this.getTweets();
    }

    getTweets() {
        let url = tweetsUrl + "?currentPage=" + this.pagination.currentPage + "&perPage=" + this.pagination.perPage;

        if (this.filter.sentiment) {
            url += "&sentiment=" + this.filter.sentiment;
        }

        this.sendRequest(RequestMethod.Get, url)
            .subscribe(response => {
                this.tweets = response
            });

        this.sendRequest(RequestMethod.Get, tweetsUrl + "/count")
            .subscribe(response => {
                this.count = response;
            });
    }

    deleteTweet(id: number) {
        this.sendRequest(RequestMethod.Delete, tweetsUrl + "/" + id)
            .subscribe(response => {
                this.getTweets();
            });

        /*if (this.count <= ((this.pagination.currentPage * this.pagination.perPage) - this.pagination.perPage)) {
            this.pagination.currentPage -= 1;
            this.getTweets();
        }*/
    }

    updateTweet(id: number, sentiment: number) {
        let data = {
            state : sentiment
        }
        this.sendRequest(RequestMethod.Put, tweetsUrl + "/" + id, data)
            .subscribe(response => {
                this.getTweets();
            });
    }

    private sendRequest(verb: RequestMethod, url: string, data?: any) : Observable<any> {
        return this.http.request(new Request({
          method: verb, url: url, body: data
        })).map(response => {
            return response.json();
        });
    }

    /*getProduct(id: number) {
        this.sendRequest(RequestMethod.Get, productsUrl + "/" + id)
            .subscribe(response => this.product = response);
    }

    getProducts() {
        let url = productsUrl + "?related=" + this.filter.related;

        if (this.filter.category) {
            url += "&category=" + this.filter.category;
        }
        if (this.filter.search) {
            url += "&search=" + this.filter.search;
        }

        this.sendRequest(RequestMethod.Get, url)
            .subscribe(response => this.products = response);
    }

    getSuppliers() {
        this.sendRequest(RequestMethod.Get, suppliersUrl)
            .subscribe(response => this.suppliers = response);
    }

    createProduct(prod: Product) {
        let data = {
            name: prod.name, category: prod.category,
            description: prod.description, price: prod.price,
            supplier: prod.supplier ? prod.supplier.supplierId : 0
        };

        this.sendRequest(RequestMethod.Post, productsUrl, data)
            .subscribe(response => {
                prod.productId = response;
                this.products.push(prod);
            });
    }

    createProductAndSupplier(prod: Product, supp: Supplier) {
        let data = {
            name: supp.name, city: supp.city, state: supp.state
        };

        this.sendRequest(RequestMethod.Post, suppliersUrl, data)
            .subscribe(response => {
                supp.supplierId = response;
                prod.supplier = supp;
                this.suppliers.push(supp);
                if (prod != null) {
                    this.createProduct(prod);
                }
            });
    }

    replaceProduct(prod: Product) {
        let data = {
            name: prod.name, category: prod.category,
            description: prod.description, price: prod.price,
            supplier: prod.supplier ? prod.supplier.supplierId : 0
        };
        this.sendRequest(RequestMethod.Put, productsUrl + "/" + prod.productId, data)
            .subscribe(response => this.getProducts());
    }

    replaceSupplier(supp: Supplier) {
        let data = {
            name: supp.name, city: supp.city, state: supp.state
        };
        this.sendRequest(RequestMethod.Put,
            suppliersUrl + "/" + supp.supplierId, data)
            .subscribe(response => this.getProducts());
    }

    updateProduct(id: number, changes: Map<string, any>) {
        let patch = [];
        changes.forEach((value, key) =>
            patch.push({ op: "replace", path: key, value: value }));

        this.sendRequest(RequestMethod.Patch, productsUrl + "/" + id, patch)
            .subscribe(response => this.getProducts());
    }

    deleteProduct(id: number) {
        this.sendRequest(RequestMethod.Delete, productsUrl + "/" + id)
            .subscribe(response => this.getProducts());
    }

    deleteSupplier(id: number) {
        this.sendRequest(RequestMethod.Delete, suppliersUrl + "/" + id)
            .subscribe(response => {
                this.getProducts();
                this.getSuppliers();
            });
    }

    product: Product;
    products: Product[];
    suppliers: Supplier[] = [];
    */

    get filter(): Filter {
        return this.filterObject;
    }

    get pagination(): Pagination {
        return this.paginationObject;
    }
}