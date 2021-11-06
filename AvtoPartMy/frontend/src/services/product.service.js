import http from "../http_common";

class ProductsService {
    get_list_prod() {
        return http.get("api/Product");
    }
    
            
}

export default new ProductsService();