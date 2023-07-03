<script setup>
import { ref, defineEmits } from "vue";
import { getSubcategoryAttributesWithValues } from "@/database/services/subcategoryService.js";

const emit = defineEmits(["filterPrice", "filterRating", "filter"]);
const props = defineProps(["subcategoryId"]);
const subcategoryAttributesWithValues = getSubcategoryAttributesWithValues(props.subcategoryId);
const price = ref({
  from: 0,
  to: 0,
});
const rating = ref(0);
const filterMap = ref({});

const updateFilters = (event, attribute, attributeValue) => {
  // Check if attribute does not exist in filter map
  // eslint-disable-next-line no-prototype-builtins
  if (!filterMap.value.hasOwnProperty(attribute)) {
    filterMap.value[attribute] = new Set([attributeValue]);
    emit("filter", filterMap.value);
    return;
  }

  // Attribute already exists in filter map - check if option is already checked or is it a new option
  if (filterMap.value[attribute].has(attributeValue)) {
    filterMap.value[attribute].delete(attributeValue);
    if (!filterMap.value[attribute].size) delete filterMap.value[attribute];
    event.target.checked = false;
    emit("filter", filterMap.value);
    return;
  }

  filterMap.value[attribute].add(attributeValue);
  emit("filter", filterMap.value);
};

function handleFilterPrice() {
  if (price.value.from > price.value.to) return;
  if (price.value.from < 0 || price.value.to < 0) return;
  if (price.value.from && price.value.to < 1) {
    emit("filter", filterMap.value);
    return;
  }

  emit("filterPrice", price.value, filterMap.value);
}

function handleFilterRating() {
  emit("filterRating", rating.value, filterMap.value);
}
</script>

<template>
  <div class="sidebar">
    <div class="item">
      <h3 class="item__title">Price</h3>
      <hr />
      <div class="price-container">
        <input
          class="price-input"
          v-model="price.from"
          type="number"
          min="0"
          @input="handleFilterPrice"
        />
        <input class="price-input" v-model="price.to" type="number" @input="handleFilterPrice" />
      </div>
    </div>
    <div class="item">
      <h3 class="item__title">Rating</h3>
      <hr />
      <!-- TODO: create slider component in case of keeping this filter -->
      <div class="slider">
        <input
          v-model="rating"
          type="range"
          min="0"
          max="5"
          step="1"
          @change="handleFilterRating"
        />
        <span>{{ rating }}</span>
      </div>
    </div>
    <div v-for="attribute in subcategoryAttributesWithValues" :key="attribute.id" class="item">
      <h3 class="item__title">{{ attribute.name }}</h3>
      <hr />
      <div v-for="attributeValue in attribute.values" :key="attributeValue.id">
        <input
          type="checkbox"
          id="lorem"
          @click="updateFilters($event, attribute.name, attributeValue.value)"
        />
        <label for="lorem">{{ attributeValue.value }} {{ attributeValue.unit || "" }}</label>
      </div>
    </div>
  </div>
</template>

<style scoped>
.sidebar {
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
  display: flex;
  flex-direction: column;
}

.item {
  margin-inline: 20px;
  margin: 1rem 2rem;
}

.item__title {
  margin: 5px;
}

hr {
  margin-bottom: 1rem;
  margin-right: 2rem;
}

.price-input {
  border: none;
  background-color: #fff;
  border-radius: 5px;
  font-size: 14px;
  margin: 5px 0;
  padding: 5px 10px;
  width: 90%;
  outline: none;
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.slider {
  display: flex;
  align-items: center;
}

input[type="range"] {
  width: 185px;
  margin-right: 10px;
}

input[type="checkbox"] {
  margin-right: 10px;
}

button {
  height: 30px;
  width: 100%;
}
</style>
