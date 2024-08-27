<script setup>
import { ref } from "vue";
import DashboardLayout from "@/layouts/DashboardLayout.vue";
import UserList from "@/components/admin/user/UserList.vue";
import OrderList from "@/components/admin/order/OrderList.vue";
import ReviewList from "@/components/admin/review/ReviewList.vue";
import SubscriberList from "@/components/admin/newsletters/SubscriberList.vue";
import CategoryDashboard from "./dashboard/CategoryDashboard.vue";
import SubcategoryDashboard from "./dashboard/SubcategoryDashboard.vue";
import PromoCodeDashboard from "./dashboard/PromoCodeDashboard.vue";
import AttributeValuesDashboard from "./dashboard/AttributeValuesDashboard.vue";
import AttributeDashboard from "./dashboard/AttributeDashboard.vue";
import ProductDashboard from "./dashboard/ProductDashboard.vue";
import ProductSpecificationForm from "@/components/admin/product-specification/ProductSpecificationForm.vue";

const tabs = [
  {
    value: "category",
    label: "Categories",
    components: [CategoryDashboard],
  },
  {
    value: "subcategory",
    label: "Subcategories",
    components: [SubcategoryDashboard],
  },
  {
    value: "attribute",
    label: "Attributes",
    components: [AttributeDashboard],
  },
  {
    value: "attribute_values",
    label: "Attribute Values",
    components: [AttributeValuesDashboard],
  },
  {
    value: "products",
    label: "Products",
    components: [ProductDashboard],
  },
  {
    value: "products-specification",
    label: "Products Specification",
    components: [ProductSpecificationForm],
  },
  {
    value: "promo_codes",
    label: "Promo Codes",
    components: [PromoCodeDashboard],
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
    value: "newsletters",
    label: "Newsletters",
    components: [SubscriberList],
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
