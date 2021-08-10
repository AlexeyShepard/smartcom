<template>
    <div>
        <b-tab title="Продукты">
            <b-table striped hover :items="ProductItems" :fields="fields">               
                <template #cell(Код)="data">
                    {{ data.item.code }}
                </template>
                <template #cell(Наименование)="data">
                    {{ data.item.name }}
                </template>
                <template #cell(Стоимость)="data">
                    {{ data.item.price + ' р.'}}
                </template>
                <template #cell(Категория)="data">
                    {{ data.item.categoryName }}
                </template>
                <template #cell(Действия)="data">
                    <b-button variant="primary" size="sm" v-b-modal.modal-add-to-busket @click="OpenAddToBusketModal(data.item.id)">В корзину</b-button>    
                </template>
            </b-table>   
        </b-tab>
        <!--Добавление товара в корзину-->
        <b-modal id="modal-add-to-busket" title="Добавление товара в корзину" 
                hide-footer
                has-modal-card
                trap-focus
                :destroy-on-hide="false"
                aria-role="dialog"
                aria-modal>
                <form action="">
                    <div class="modal-card" style="width: 450px">
                        <section class="modal-card-body">
                            <b-field label="Дата завершения">
                                <b-form-spinbutton
                                    required
                                    min=1
                                    max=100
                                    v-model="count">
                                </b-form-spinbutton>
                            </b-field>                            
                        </section>
                    <footer class="modal-card-foot">
                        <b-button class="mt-3" variant="outline-primary" block @click="AddProductToBusket">Добавить</b-button>
                    </footer>
                </div>       
                </form>            
        </b-modal>        
    </div>
</template>

<script>

import axios from 'axios'

export default {
    data() {
        return {
            id: 0,
            count:1,
            fields:[
                'Код',
                'Наименование',
                'Стоимость',
                'Категория',
                'Действия'
            ],
            ProductItems:[]
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
    },
    methods: {
        AddProductToBusket() {
            var Busket = JSON.parse("[" + localStorage.getItem("Busket") + "]");
            for(var i = 0; i < this.count; i++) Busket.push(this.id);
            localStorage.removeItem("Busket");
            localStorage.setItem("Busket", Busket);           

            location.reload();
        },
        OpenAddToBusketModal(id) {
            this.id = id,
            this.count = 1
        }

    }
}

</script>
