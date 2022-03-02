<template>
  <div>
    <div class="slideout">
      <div class="slideout-header">
        <button href="#" class="button is-ghost slideout-close" title="Close" @click="props.toggleSearchDrawer">
          <i class="fa fa-times fa-lg"> </i>
        </button>
      </div>
      <div class="columns">
        <div class="column is-10 is-offset-1">
          <form class="p-3" @submit.prevent="handleSearch">
            <div class="is-flex is-justify-content-space-between">
              <div class="field is-fullwidth">
                <label class="label">Min. Year</label>
                <div class="control">
                  <div class="select is-fullwidth">
                    <select v-model="formData.minYear">
                      <option selected value="">Select Year</option>
                      <option v-for="option in yearOptions" v-bind:value="option.value">
                        {{ option.text }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
              <div class="field is-fullwidth ml-3">
                <label class="label">Max. Year</label>
                <div class="control">
                  <div class="select is-fullwidth">
                    <select v-model="formData.maxYear">
                      <option selected value="">Select Year</option>
                      <option v-for="option in yearOptions" v-bind:value="option.value">
                        {{ option.text }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
            </div>

            <DynamicDropdown label="Make" />
            <DynamicDropdown label="Model" :disabled="true" />

            <button :class="{ 'is-loading': searching }" type="submit" class="button is-success is-outlined mt-3">Search</button>
          </form>
        </div>
      </div>
    </div>
    <div class="slideout-mask"></div>
  </div>
</template>
<script setup lang="ts">
import { computed, PropType, ref } from "vue";
import { SearchQuery } from "../models/searchQuery";
import { getYearOptions } from "../utils/options";
import DynamicDropdown from "./DynamicDropdown.vue";

const props = defineProps({
  toggleSearchDrawer: {
    type: Function as PropType<(payload: MouseEvent) => void>,
    required: true,
  },
});

const formData = ref<SearchQuery>({ minYear: "1960", maxYear: new Date().getUTCFullYear().toString() });
const errors = ref<string>();
const searching = ref<boolean>(false);

const yearOptions = computed(() => getYearOptions());

function handleSearch() {
  searching.value = true;
  console.log("handleSearch");

  searching.value = false;
}
</script>
<style lang="scss" scoped>
$gray: #868e96;
$gray-light: lighten($gray, 10%);
$gray-lighter: lighten($gray-light, 10%);

@mixin drop-shadow($x: 0, $y: 1px, $blur: 2px, $spread: 0, $alpha: 0.25) {
  box-shadow: $x $y $blur $spread rgba(0, 0, 0, $alpha);
}

@mixin opacity($opacity: 0.5) {
  opacity: $opacity;
}

.is-fullwidth {
  width: 100%;
}

.slideout {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  width: 35em;
  background-color: #ffffff;
  border-right: 0.063em solid lighten($gray-light, 30%);
  @include drop-shadow(1px, 0, 8px, 0, 0.5);
  z-index: 1000;
  transform: translateX(-100%);
  transition: transform 0.3s ease, width 0.3s ease;

  .slideout-header {
    padding: 1.2em 1em 1.2em 1.7em;
    display: flex;
    justify-content: flex-end;

    .slideout-close {
      display: inline-block;
    }
  }
}

.slideout-mask {
  position: fixed;
  overflow: hidden;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #000000;
  z-index: 999;
  @include opacity(0);
  visibility: hidden;
  transition: opacity 0.3s ease, visibility 0.3s ease;
}

.slideout-open {
  .slideout-mask {
    @include opacity(0.7);
    visibility: visible;
  }

  .slideout {
    transform: translateX(0);
  }
}

@media (max-width: 768px) {
  .slideout-open {
    .slideout {
      width: 100vw;
    }
  }
}
</style>
