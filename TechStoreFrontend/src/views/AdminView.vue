<script setup>
import { ref } from "vue";
import DashboardLayout from "@/layouts/DashboardLayout.vue";
import CategoryForm from "@/components/admin/category/CategoryForm.vue";
import CategoryList from "@/components/admin/category/CategoryList.vue";
import SubcategoryForm from "@/components/admin/subcategory/SubcategoryForm.vue";
import SubcategoryList from "@/components/admin/subcategory/SubcategoryList.vue";
import AttributeForm from "@/components/admin/attribute/AttributeForm.vue";
import AttributeList from "@/components/admin/attribute/AttributeList.vue";
import AttributeValuesForm from "@/components/admin/attribute_values/AttributeValuesForm.vue";
import AttributeValuesList from "@/components/admin/attribute_values/AttributeValuesList.vue";
import ProductForm from "@/components/admin/product/ProductForm.vue";
import ProductList from "@/components/admin/product/ProductList.vue";
import UserList from "@/components/admin/user/UserList.vue";
import OrderList from "@/components/admin/order/OrderList.vue";
import ReviewList from "@/components/admin/review/ReviewList.vue";
import PromoCodeForm from "@/components/admin/promo_code/PromoCodeForm.vue";
import PromoCodeList from "@/components/admin/promo_code/PromoCodeList.vue";

const tabs = [
  {
    value: "category",
    label: "Categories",
    components: [CategoryForm, CategoryList],
  },
  {
    value: "subcategory",
    label: "Subcategories",
    components: [SubcategoryForm, SubcategoryList],
  },
  {
    value: "attribute",
    label: "Attributes",
    components: [AttributeForm, AttributeList],
  },
  {
    value: "attribute_values",
    label: "Attribute Values",
    components: [AttributeValuesForm, AttributeValuesList],
  },
  {
    value: "products",
    label: "Products",
    components: [ProductForm, ProductList],
  },
  {
    value: "users",
    label: "Users",
    components: [UserList],
  },
  {
    value: "orders",
    label: "Orders",
    components: [OrderList],
  },
  {
    value: "reviews",
    label: "Reported Reviews",
    components: [ReviewList],
  },
  {
    value: "promo_codes",
    label: "Promo Codes",
    components: [PromoCodeForm, PromoCodeList],
  },
];

const tab = ref("category");
</script>

<template>
  <dashboard-layout>
    <template v-slot:tabs>
      <v-tabs v-model="tab" direction="vertical">
        <v-tab class="tab" v-for="(tab, index) in tabs" :key="index" :value="tab.value">
          {{ tab.label }}
        </v-tab>
      </v-tabs>
    </template>
    <template v-slot:tab>
      <v-window v-model="tab">
        <v-window-item v-for="(tab, index) in tabs" :key="index" :value="tab.value">
          <component v-for="(component, index) in tab.components" :key="index" :is="component" />
        </v-window-item>
      </v-window>
    </template>
  </dashboard-layout>
</template>

<style scoped>
.tab {
  background: var(--ts-c-primary-soft);
  margin-bottom: 1rem;
}

.v-slide-group-item--active {
  background: var(--ts-c-primary-mute);
}
</style>
