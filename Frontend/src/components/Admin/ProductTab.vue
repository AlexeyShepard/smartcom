<template>
    <div>
        <b-tab title="Продукты">
            <b-button variant="primary modal-button" v-b-modal.modal-add-product>Добавить</b-button>
            <b-table striped hover :items="ProductItems" :fields="fields">               
                <template #cell(Id)="data">
                    {{ data.item.id }}
                </template>
                <template #cell(Наименование)="data">
                    {{ data.item.name }}
                </template>
                <template #cell(Код)="data">
                    {{ data.item.code }}
                </template>
                <template #cell(Стоимость)="data">
                    {{ data.item.price + ' р.'}}
                </template>
                <template #cell(Категория)="data">
                    {{ data.item.categoryName }}
                </template>
                <template #cell(Действия)="data">
                    <b-button variant="primary" size="sm" v-b-modal.modal-edit-product @click="OpenEditProductModal(data.item)">Редактировать</b-button>
                    <b-button variant="danger" size="sm" @click="DeleteProduct(data.item.id)">Удалить</b-button>
                </template>
            </b-table>
        </b-tab>
        <!--Создание продукта-->
        <b-modal id="modal-add-product" title="Добавление продукта" 
                hide-footer
                has-modal-card
                trap-focus
                :destroy-on-hide="false"
                aria-role="dialog"
                aria-modal>
                <form action="">
                    <div class="modal-card" style="width: 450px">
                        <section class="modal-card-body">
                            <b-field label="Наименование">
                                <b-input
                                    type="Name"
                                    :value="name"
                                    password-reveal
                                    placeholder="Наименование"
                                    required
                                    v-model="name">
                                </b-input>
                            </b-field>

                            <b-field label="Стоимость">
                                <b-input
                                    type="number"
                                    min=0
                                    max=99999
                                    :value="price"
                                    placeholder="Стоимость"
                                    required
                                    v-model="price">
                                </b-input>
                            </b-field>

                            <b-field label="Категория">
                                <b-form-select
                                    :options="Categories"
                                    v-model="categoryId"
                                    required>
                                </b-form-select>
                            </b-field>                            
                        </section>
                    <footer class="modal-card-foot">
                        <b-button class="mt-3" variant="outline-primary" block @click="AddProduct">Добавить</b-button>
                    </footer>
                </div>       
                </form>            
        </b-modal>
        <!--Редактирование продукта-->
        <b-modal id="modal-edit-product" title="Редактирование продукта" 
                hide-footer
                has-modal-card
                trap-focus
                :destroy-on-hide="false"
                aria-role="dialog"
                aria-modal>
                <form action="">
                    <div class="modal-card" style="width: 450px">
                        <section class="modal-card-body">
                            <b-field label="Наименование">
                                <b-input
                                    type="Name"
                                    :value="name"
                                    password-reveal
                                    placeholder="Наименование"
                                    required
                                    v-model="name">
                                </b-input>
                            </b-field>

                            <b-field label="Стоимость">
                                <b-input
                                    type="number"
                                    min=0
                                    max=99999
                                    :value="price"
                                    placeholder="Стоимость"
                                    required
                                    v-model="price">
                                </b-input>
                            </b-field>

                            <b-field label="Категория">
                                <b-form-select
                                    :options="Categories"
                                    v-model="categoryId"
                                    required>
                                </b-form-select>
                            </b-field>                            
                        </section>
                    <footer class="modal-card-foot">
                        <b-button class="mt-3" variant="outline-primary" block @click="EditProduct">Изменить</b-button>
                    </footer>
                </div>       
                </form>            
        </b-modal>
    </div>
</template>

<script>

import axios from 'axios';

export default {
    data() {
        return {
            id:0,
            code:'11-1111-AS11',
            name:'',
            price: 0,
            categoryId: 0,
            fields:[
                'Id',
                'Наименование',
                'Код',
                'Стоимость',
                'Категория',
                'Действия'
            ],
            ProductItems: [],
            Categories: []
        }
    },
    created() {
        axios.get(this.$ApiUrl + '/Product',
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .then((response) => {
                    this.ProductItems = response.data;                                    
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });

        axios.get(this.$ApiUrl + '/Product/Category',
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .then((response) => {
                    this.Categories = response.data;                                    
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });
        
    },
    methods: {

        async AddProduct() {
            await axios.post(this.$ApiUrl + '/Product', {
                    name: this.name,
                    price: Number.parseInt(this.price),
                    categoryId: this.categoryId,
                    code: this.code
                },
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")  
                    }
                },
                {
                    withCredentials: true
                })
                .then(function(response) {
                        alert("Продукт успешно изменен!");             
                })
                .catch(function (error) {
                        alert(error);
                });
                
            location.reload();
        },

        async EditProduct() {

            await axios.put(this.$ApiUrl + '/Product', {
                    id: this.id,
                    name: this.name,
                    price: Number.parseInt(this.price),
                    categoryId: this.categoryId,
                    code: this.code
                },
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")  
                    }
                },
                {
                    withCredentials: true
                })
                .then(function(response) {
                        alert("Продукт успешно добавлен!");             
                })
                .catch(function (error) {
                        alert(error);
                });
                
            location.reload();
        },

        async DeleteProduct(id) {
            await axios.delete(this.$ApiUrl+ '/Product/' + id,
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });
                
                location.reload();    
        },

        OpenEditProductModal(ProductData) {
            alert(ProductData);

            this.id = ProductData.id;
            this.name = ProductData.name;
            this.price = ProductData.price;
            this.categoryId = ProductData.categoryId;
            this.code = ProductData.code;
        }
    }

}

</script>
